﻿/*
 * Copyright (c) 2018 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using System.Text;
using SafeExamBrowser.Configuration.DataFormats.Cryptography;
using SafeExamBrowser.Contracts.Configuration;
using SafeExamBrowser.Contracts.Configuration.Settings;
using SafeExamBrowser.Contracts.Logging;

namespace SafeExamBrowser.Configuration.DataFormats
{
	public partial class BinaryFormat : IDataFormat
	{
		private const int PREFIX_LENGTH = 4;

		private IDataCompressor compressor;
		private IHashAlgorithm hashAlgorithm;
		private IModuleLogger logger;

		public BinaryFormat(IDataCompressor compressor, IHashAlgorithm hashAlgorithm, IModuleLogger logger)
		{
			this.compressor = compressor;
			this.hashAlgorithm = hashAlgorithm;
			this.logger = logger;
		}

		public bool CanParse(Stream data)
		{
			try
			{
				var longEnough = data.Length > PREFIX_LENGTH;

				if (longEnough)
				{
					var prefix = ParsePrefix(data);
					var success = TryDetermineFormat(prefix, out FormatType format);

					logger.Debug($"'{data}' starting with '{prefix}' does {(success ? string.Empty : "not ")}match the binary format.");

					return success;
				}

				logger.Debug($"'{data}' is not long enough ({data.Length} bytes) to match the binary format.");
			}
			catch (Exception e)
			{
				logger.Error($"Failed to determine whether '{data}' with {data.Length / 1000.0} KB data matches the binary format!", e);
			}

			return false;
		}

		public LoadStatus TryParse(Stream data, Settings settings, string password = null, bool passwordIsHash = false)
		{
			var prefix = ParsePrefix(data);
			var success = TryDetermineFormat(prefix, out FormatType format);

			if (success)
			{
				if (compressor.IsCompressed(data))
				{
					data = compressor.Decompress(data);
				}

				logger.Debug($"Attempting to parse '{data}' with format '{prefix}'...");
				data = new SubStream(data, PREFIX_LENGTH, data.Length - PREFIX_LENGTH);

				switch (format)
				{
					case FormatType.Password:
					case FormatType.PasswordConfigureClient:
						return ParsePasswordBlock(data, format, settings, password, passwordIsHash);
					case FormatType.PlainData:
						return ParsePlainDataBlock(data, settings);
					case FormatType.PublicKeyHash:
						return ParsePublicKeyHashBlock(data, settings, password, passwordIsHash);
					case FormatType.PublicKeyHashWithSymmetricKey:
						return ParsePublicKeyHashWithSymmetricKeyBlock(data, settings, password, passwordIsHash);
				}
			}

			logger.Error($"'{data}' starting with '{prefix}' does not match the binary format!");

			return LoadStatus.InvalidData;
		}

		private LoadStatus ParsePasswordBlock(Stream data, FormatType format, Settings settings, string password, bool passwordIsHash)
		{
			var decrypted = default(Stream);
			var encryption = new PasswordEncryption(logger.CloneFor(nameof(PasswordEncryption)));
			var status = default(LoadStatus);

			if (format == FormatType.PasswordConfigureClient)
			{
				password = password == null ? string.Empty : (passwordIsHash ? password : hashAlgorithm.GenerateHashFor(password));
				status = encryption.Decrypt(data, out decrypted, password);

				if (status == LoadStatus.PasswordNeeded && passwordIsHash && password != string.Empty)
				{
					status = encryption.Decrypt(data, out decrypted, string.Empty);
				}
			}
			else
			{
				status = encryption.Decrypt(data, out decrypted, password);
			}

			if (status == LoadStatus.Success)
			{
				return ParsePlainDataBlock(decrypted, settings);
			}

			return status;
		}

		private LoadStatus ParsePlainDataBlock(Stream data, Settings settings)
		{
			var xmlFormat = new XmlFormat(logger.CloneFor(nameof(XmlFormat)));

			if (compressor.IsCompressed(data))
			{
				data = compressor.Decompress(data);
			}

			return xmlFormat.TryParse(data, settings);
		}

		private LoadStatus ParsePublicKeyHashBlock(Stream data, Settings settings, string password, bool passwordIsHash)
		{
			var encryption = new PublicKeyHashEncryption(logger.CloneFor(nameof(PublicKeyHashEncryption)));
			var status = encryption.Decrypt(data, out Stream decrypted);

			if (status == LoadStatus.Success)
			{
				return TryParse(decrypted, settings, password, passwordIsHash);
			}

			return status;
		}

		private LoadStatus ParsePublicKeyHashWithSymmetricKeyBlock(Stream data, Settings settings, string password, bool passwordIsHash)
		{
			var logger = this.logger.CloneFor(nameof(PublicKeyHashWithSymmetricKeyEncryption));
			var passwordEncryption = new PasswordEncryption(logger.CloneFor(nameof(PasswordEncryption)));
			var encryption = new PublicKeyHashWithSymmetricKeyEncryption(logger, passwordEncryption);
			var status = encryption.Decrypt(data, out Stream decrypted);

			if (status == LoadStatus.Success)
			{
				return TryParse(decrypted, settings, password, passwordIsHash);
			}

			return status;
		}

		private string ParsePrefix(Stream data)
		{
			var prefixData = new byte[PREFIX_LENGTH];

			if (compressor.IsCompressed(data))
			{
				prefixData = compressor.Peek(data, PREFIX_LENGTH);
			}
			else
			{
				data.Seek(0, SeekOrigin.Begin);
				data.Read(prefixData, 0, PREFIX_LENGTH);
			}

			return Encoding.UTF8.GetString(prefixData);
		}

		private bool TryDetermineFormat(string prefix, out FormatType format)
		{
			format = default(FormatType);

			switch (prefix)
			{
				case "pswd":
					format = FormatType.Password;
					return true;
				case "pwcc":
					format = FormatType.PasswordConfigureClient;
					return true;
				case "plnd":
					format = FormatType.PlainData;
					return true;
				case "pkhs":
					format = FormatType.PublicKeyHash;
					return true;
				case "phsk":
					format = FormatType.PublicKeyHashWithSymmetricKey;
					return true;
			}

			return false;
		}

		private enum FormatType
		{
			Password = 1,
			PasswordConfigureClient,
			PlainData,
			PublicKeyHash,
			PublicKeyHashWithSymmetricKey
		}
	}
}
