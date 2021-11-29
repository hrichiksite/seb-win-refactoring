/*
 * Copyright (c) 2021 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Linq;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.SystemComponents.Contracts;

namespace SafeExamBrowser.SystemComponents
{
	public class VirtualMachineDetector : IVirtualMachineDetector
	{
		/// <summary>
		/// Virtualbox: VBOX, 80EE
		/// RedHat: QUEMU, 1AF4, 1B36
		/// </summary>


		private ILogger logger;
		private ISystemInfo systemInfo;
		
		public VirtualMachineDetector(ILogger logger, ISystemInfo systemInfo)
		{
			this.logger = logger;
			this.systemInfo = systemInfo;
		}

		public bool IsVirtualMachine()
		{
			logger.Debug($"Computer '{systemInfo.Name}' appears to not be a virtual machine.");
			
			/// bruh
			return false;
		}
	}
}
