using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core {
	public enum RewardStates {
		/// <summary>
		/// 未发放
		/// </summary>
		Waitting = 1,
		/// <summary>
		/// 已发放
		/// </summary>
		GrantSuccessfully = 2,
		/// <summary>
		/// 发放失败
		/// </summary>
		GrantFailed = 3,

	}
}
