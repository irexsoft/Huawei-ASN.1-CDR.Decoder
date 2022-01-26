using System;
using System.Collections.Generic;

namespace CDR.Decoder.Parselets
{
    class d900_PartialRecordTypeParselet : GenericParselet
    {
        private static Dictionary<int, string> _types = new Dictionary<int, string>(){
            {0, "timeLimit"},
            {1, "serviceChange"},
            {2, "locationChange"},
            {3, "classmarkChange"},
            {4, "aocParmChange"},
            {5, "radioChannelChange"},
            {6, "hSCSDParmChange"},
            {7, "changeOfCAMELDestination"},
            {8, "callTransferInvocation"},
            {9, "changeOfChargeParam"},
            {10, "changeOfCAMELDestinationSubsequentDialogue"},
            {11, "interPLMNHandover"},
            {12, "maxNumberofInterPlmnHandovers"},
            {14, "changeOfDestination"},
            {255, "not-applicable"}
        };

        public d900_PartialRecordTypeParselet()
            : base()
        {
            RegisterMethod("Int", ValueAsInteger);
            RegisterMethod("Type", ValueAsTypeName);
            DefaultValueType = "Type";
        }

        public static string ValueAsInteger(byte[] value)
        {
            return IntegerParselet.ValueAsInteger(value);
        }

        public static string ValueAsTypeName(byte[] value)
        {
            if (value == null)
                return String.Empty;
            int v = IntegerParselet.ArrayToInt32(value);

            if (_types.ContainsKey(v))
            {
                return _types[v];
            }
            return _types[255];
        }
    }
}
