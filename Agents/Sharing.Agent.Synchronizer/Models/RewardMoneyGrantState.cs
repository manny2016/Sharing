using System;
using System.Collections.Generic;
using System.Text;
using Sharing.Agent.Synchronizer.Services;
using Sharing.Core;
using Sharing.Core.Models;

namespace Sharing.Agent.Synchronizer.Models {
	public class RewardMoneyGrantState : ProcessState<RewardLogging> {
		private IProcessingResultService<RewardLogging> service;
		public override string Name {
			get {
				return "Reward Money Grant.";
			}
		}
		public RewardMoneyGrantState(RewardMoneyGrantSetting setting) : base(setting) {

		}
		public override IProcessingResultService<RewardLogging> GenerateProcessingResultService() {
			return new RewardMoneyGrantResultService(this.Setting as RewardMoneyGrantSetting);
		}
	}
}
