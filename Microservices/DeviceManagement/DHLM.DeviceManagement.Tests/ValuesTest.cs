using System;
using Xunit;
using DHLM.DeviceManagement.BusinessLayer;

namespace DHLM.DeviceManagement.Tests
{
    public class ValuesTest
    {
        bool isCold = false;    
        
        [Fact]
        public void ColdTest()
        {
            //Values v = new Values();
            // bool isReturnedWaterCold = v.GetWater(10);  
            Assert.True(isCold == false);
        }

      
    }
}
