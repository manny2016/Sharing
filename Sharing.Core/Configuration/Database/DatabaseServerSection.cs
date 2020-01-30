
using Microsoft.Extensions.Configuration;
namespace Sharing.Core.Configuration
{
    public class DatabaseServer  {
		
		
		public string Server
        {
           get;set;
        }

        
        public bool IsWriteOnly
        {
			get; set;
		}
        /// <summary>
        /// 
        /// </summary>
        
        public string UserId
        {
			get; set;
		}
        /// <summary>
        /// 
        /// </summary>
        
        public string Password
        {
			get; set;
		}
        /// <summary>
        /// 
        /// </summary>
        
        public DatabaseTypes Type
        {
			get; set;
		}

	
	}
}
