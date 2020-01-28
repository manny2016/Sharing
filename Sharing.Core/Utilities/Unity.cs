using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;
using Sharing.Core.Models;

namespace Sharing.Core {
	public static class Unity {
		public static SqlDataRecord Convert(RegisterWxUserContext model) {
			if ( model == null ) { return null; }
			var record = new SqlDataRecord(Constants.RegisterWeChatUserStructure);
			for ( var i = 0; i < Constants.RegisterWeChatUserStructure.Length; i++ ) {
				switch ( Constants.RegisterWeChatUserStructure[i].Name ) {
					case "UnionId":
						record.SetString(i, model.Info.UnionId);
						break;
					case "AppId":
						record.SetString(i, model.WxApp.AppId);
						break;
					case "OpenId":
						record.SetString(i, model.Info.OpenId);
						break;
					case "RegistrySource":
						record.SetInt32(i, (int)model.WxApp.AppType);
						break;
					case "NickName":
						if ( string.IsNullOrEmpty(model.Info.NickName) ) {
							record.SetDBNull(i);
						} else {
							record.SetString(i, model.Info.NickName);
						}
						break;
					case "Country":
						if ( string.IsNullOrEmpty(model.Info.Country) ) {
							record.SetDBNull(i);
						} else {
							record.SetString(i, model.Info.Country);
						}
						break;
					case "Province":
						if ( string.IsNullOrEmpty(model.Info.Province) ) {
							record.SetDBNull(i);
						} else {
							record.SetString(i, model.Info.Province);
						}
						break;
					case "City":
						if ( string.IsNullOrEmpty(model.Info.City) ) {
							record.SetDBNull(i);
						} else {
							record.SetString(i, model.Info.City);
						}
						break;
					case "AvatarUrl":
						if ( string.IsNullOrEmpty(model.Info.AvatarUrl) ) {
							record.SetDBNull(i);
						} else {
							record.SetString(i, model.Info.AvatarUrl);
						}
						break;
					case "LastUpdatedBy":
						if ( string.IsNullOrEmpty(model.LastUpdateBy) ) {
							record.SetDBNull(i);
						} else {
							record.SetString(i, model.Info.AvatarUrl);
						}
						break;
					case "ScenarioId":
						if ( model.ScenarioId == null ) {
							record.SetDBNull(i);
						} else {
							record.SetGuid(i, model.ScenarioId.Value);
						}
						break;
				}
			}
			return record;
		}
	}
}
