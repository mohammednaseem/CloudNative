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
            //Assert.True(true != isReturnedWaterCold);
        }

        [Fact]
        public void HotTest()
        {
           // Values v = new Values();
           // bool isReturnedWaterCold = v.GetWater(11);  
           // Assert.True(isCold != isReturnedWaterCold);
        }
    }
}
