using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Address;
using AddressLabelUtilityCore.Label;

namespace AddressLabelUtilityCore.Helper
{
    public static class BuildingLabelContentHelper
    {
        public static LabelContent Build(IAddress toAddress, IAddress fromAddress)
        {
            return new LabelContent(toAddress, fromAddress);
        }

        public static IEnumerable<LabelContent> Build(IEnumerable<IAddress> toAddressList, IAddress fromAddress)
        {
            return toAddressList
                .Select(x => new LabelContent(new DefaultAddress(x), new DefaultAddress(fromAddress)));
        }
    }
}
