using System;
using Xunit;


namespace DHLM.DeviceManagement.Tests
{
    public class WaterTest
    {
        string coldWater = "Cold Water";
        string hotWater  = "Hot Water";
        
        [Fact]
        public void ColdTest()
        {
           // Water v = new Water();
           // string waterType = v.GetWater(10);  
            Assert.Equal("Cold Water", coldWater);
        }

        [Fact]
        public void HotTest()
        {
           // Water v = new Water();
           // string waterType = v.GetWater(11);  
           Assert.Equal("Hot Water", hotWater);
        }
    }
}
