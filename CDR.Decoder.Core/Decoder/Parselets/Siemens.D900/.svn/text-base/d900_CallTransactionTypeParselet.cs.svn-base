using System;
using System.Collections.Generic;

namespace CDR.Decoder.Parselets
{
    class d900_CallTransactionTypeParselet : GenericParselet
    {
        private static Dictionary<int, string> _typesA = new Dictionary<int, string>()
        {
            {0,"default"},
            {1,"moc"},
            {2,"mtc"},
            {3,"emergencyCall"},
            {4,"pbxOutgoingCall"},
            {5,"pbxIncomingCall"},
            {6,"mocOACSU"},
            {8,"inCallModMoc"},
            {9,"inCallModMtc"},
            {10,"sSRegistration"},
            {11,"sSErasure"},
            {12,"sSActivation"},
            {13,"sSDeactivation"},
            {14,"sSInterrogation"},
            {15,"sSInvocation"},
            {16,"mocSMS"},
            {17,"mtcSMS"},
            {20,"mocDPAD"},
            {21,"inAttempt"},
            {22,"inAnswered"},
            {26,"roaming"},
            {27,"transit"},
            {29,"callForwarding"},
            {32,"emergencyCallTrace"},
            {34,"roaAttempt"},
            {35,"mtLocationRequest"},
            {36,"mtLocationRequestAttempt"},
            {37,"moLocationRequest"},
            {38,"moLocationRequestAttempt"},
            {39,"niLocationRequest"},
            {40,"niLocationRequestAttempt"},
            {42,"camelCallForwarding"},
            {43,"termCAMELRecord"},
            {44,"voiceGroupServiceAMSC"},
            {45,"termCAMELIntAttemptRecord"},
            {46,"voiceGroupServiceRMSC"},
            {58,"sSUnstructuredProcessingPh1"},
            {59,"processUnstructuredSSRequestMo"},
            {60,"unstucturedSSRequestNi"},
            {61,"unstucturedSSNotifyNi"},
            {65,"mocAttempt"},
            {66,"mtcAttempt"},
            {67,"emyAttempt"}
        };
        private static Dictionary<int, string> _typesB = new Dictionary<int, string>()
        {
            { 0,"default"},
            { 1,"moc"},
            { 2,"mtc"},
            { 3,"emergencyCall"},
            { 4,"pbxOutgoingCall"},
            { 5,"pbxIncomingCall"},
            { 6,"mocOACSU"},
            { 8,"inCallModMoc"},
            { 9,"inCallModMtc"},
            {10,"sSRegistration"},
            {11,"sSErasure"},
            {12,"sSActivation"},
            {13,"sSDeactivation"},
            {14,"sSInterrogation"},
            {15,"sSUnstructuredProcessingPh1"},
            {19,"mODPAD"},
            {26,"roaming"},
            {27,"transit"},
            {29,"callForwarding"},
            {30,"sMS-MTC"},
            {31,"sMS-MOC"},
            {32,"emergencyCallTrace"},
            {33,"sSInvocation"},
            {34,"roaAttempt"},
            {35,"mtLocationRequest"},
            {36,"mtLocationRequestAttempt"},
            {37,"moLocationRequest"},
            {38,"moLocationRequestAttempt"},
            {39,"niLocationRequest"},
            {40,"niLocationRequestAttempt"},
            {43,"termCAMELIntRecord"},
            {44,"voiceGroupServiceAMSC"},
            {45,"termCAMELIntAttemptRecord"},
            {46,"voiceGroupServiceRMSC"},
            {59,"processUnstructuredSSRequestMo"},
            {60,"unstucturedSSRequestNi"},
            {61,"unstucturedSSNotifyNi"},
            {65,"mocAttempt"},
            {66,"mtcAttempt"},
            {67,"emyAttempt"},
            {93,"cFAttempt"}
        };
        private static Dictionary<int, string> _typesC = new Dictionary<int, string>()
        {
            { 0,"default"},
            { 1,"moc"},
            { 2,"mtc"},
            { 3,"emergencyCall"},
            { 6,"mocOACSU"},
            { 8,"inCallModMoc"},
            { 9,"inCallModMtc"},
            {10,"sSRegistration"},
            {11,"sSErasure"},
            {12,"sSActivation"},
            {13,"sSDeactivation"},
            {14,"sSInterrogation"},
            {15,"sSUnstructuredProcessingPh1"},
            {19,"mODPAD"},
            {26,"roaming"},
            {27,"transit"},
            {29,"callForwarding"},
            {30,"mtcSMS"},
            {31,"mocSMS"},
            {32,"emergencyCallTrace"},
            {33,"sSInvocation"},
            {34,"roaAttempt"},
            {35,"mtLocationRequest"},
            {36,"mtLocationRequestAttempt"},
            {37,"moLocationRequest"},
            {38,"moLocationRequestAttempt"},
            {39,"niLocationRequest"},
            {40,"niLocationRequestAttempt"},
            {43,"termCAMELIntRecord"},
            {44,"voiceGroupServiceAMSC"},
            {45,"termCAMELIntAttemptRecord"},
            {46,"voiceGroupServiceRMSC"},
            {59,"processUnstructuredSSRequestMo"},
            {60,"unstucturedSSRequestNi"},
            {61,"unstucturedSSNotifyNi"},
            {65,"mocAttempt"},
            {66,"mtcAttempt"},
            {67,"emyAttempt"},
            {93,"cfAttempt"},
            {129,"sUBOG"},
            {130,"sUBIC"},
            {145,"pBXOG"},
            {146,"pBXIC"},
            {154,"pBX-SS-REG"},
            {155,"pBX-SS-ERAS"},
        };

        public d900_CallTransactionTypeParselet()
            : base()
        {
            RegisterMethod("Int", ValueAsInteger);
            RegisterMethod("Type_Coding_A", ValueAsTypeNameA);
            RegisterMethod("Type_Coding_B", ValueAsTypeNameB);
            RegisterMethod("Type_Coding_C", ValueAsTypeNameC);
            DefaultValueType = "Type_Coding_C";
        }

        public static string ValueAsInteger(byte[] value)
        {
            return IntegerParselet.ValueAsInteger(value);
        }

        public static string ValueAsTypeNameA(byte[] value)
        {
            if (value == null)
                return String.Empty;
            int v = IntegerParselet.ArrayToInt32(value);

            if (_typesA.ContainsKey(v))
            {
                return _typesA[v];
            }
            return _typesA[0];
        }

        public static string ValueAsTypeNameB(byte[] value)
        {
            if (value == null)
                return String.Empty;
            int v = IntegerParselet.ArrayToInt32(value);

            if (_typesB.ContainsKey(v))
            {
                return _typesB[v];
            }
            return _typesB[0];
        }

        public static string ValueAsTypeNameC(byte[] value)
        {
            if (value == null)
                return String.Empty;
            int v = IntegerParselet.ArrayToInt32(value);

            if (_typesC.ContainsKey(v))
            {
                return _typesC[v];
            }
            return _typesC[0];
        }
    }
}
