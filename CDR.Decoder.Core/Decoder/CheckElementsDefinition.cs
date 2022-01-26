using System;
using System.Collections.Generic;
using System.IO;
using CDR.Schema;
using CDR.Decoder.BER;

namespace CDR.Decoder
{
    using ElementsStatTable = SortedDictionary<string, int>;

    partial class CdrDecoder
    {
        protected void CheckElement(CdrElement element, ElementsStatTable tablePresent, ElementsStatTable tableNone)
        {
            if (element.Path.Equals(element.Name) || (element.Name.Length == 0))
            {
                if (tableNone.ContainsKey(element.Path))
                {
                    tableNone[element.Path]++;
                }
                else
                {
                    tableNone.Add(element.Path, 1);
                }
            }
            else
            {
                if (tablePresent.ContainsKey(element.Path))
                {
                    tablePresent[element.Path]++;
                }
                else
                {
                    tablePresent.Add(element.Path, 1);
                }
            }

            if (element.IsConstructed && !element.IsEmpty)
            {
                foreach (CdrElement child in (element.Value as List<CdrElement>))
                    CheckElement(child, tablePresent, tableNone);
            }
        }

        public int CheckElementsDefinition(Stream asnStream, TextWriter dumpWriter)
        {
            long offset = 0;

            BerDecoderResult pr = BerDecoderResult.Finished;
            TlvObject tlv;
            CdrElement record;
            int cnt = 0;

            ElementPathComparer comparer = new ElementPathComparer();

            ElementsStatTable present = new ElementsStatTable(comparer);
            ElementsStatTable none = new ElementsStatTable(comparer);

            dumpWriter.WriteLine("Check elements definition.\n==========================");
            dumpWriter.WriteLine("Current configuration: {0}\n", ElementDefinitionProvider.CurrentSchema);

            int rem;

            dumpWriter.WriteLine("Please wait");
            for (; ; )//(int n = 5000; n > 0; n--)
            {
                pr = DecodeTlv(asnStream, out tlv, ref offset, 0, Byte.MaxValue);

                if (pr != BerDecoderResult.Finished)
                    break;

                record = _tlvParser.ParseTlvObject(tlv);
                CheckElement(record, present, none);
                cnt++;

                Math.DivRem(cnt, 1000, out rem);

                if (rem == 0) dumpWriter.Write('.');
            }
            dumpWriter.WriteLine("\n");

            int e_sum = 0;
            int max_present_value = 0;

            foreach (var value in present)
            {
                e_sum += value.Value;
                if (max_present_value < value.Value)
                {
                    max_present_value = value.Value;
                }
            }

            int max_none_value = 0;

            foreach (var value in none)
            {
                e_sum += value.Value;
                if (max_none_value < value.Value)
                {
                    max_none_value = value.Value;
                }
            }

            dumpWriter.WriteLine("- Total {0} records processed ({1} elements)\n", cnt, e_sum);

            dumpWriter.WriteLine("- The elements are present:\n===========================");
            string headerFmt = " {0,-15}| {1, -30}| {2}";
            dumpWriter.WriteLine(headerFmt, "Path", "Name", "Count");
            dumpWriter.WriteLine(headerFmt
                , "---------------"
                , "------------------------------"
                , "---------------------------"
                );

            ICdrElementDefinition elementDef;
            foreach (var value in present)
            {
                elementDef = ElementDefinitionProvider.FindDefinition(value.Key);
                dumpWriter.Write(headerFmt, value.Key, (elementDef != null) ? elementDef.Name : String.Empty, string.Empty);
                for (int n = 1; n < value.Value * 26 / max_present_value; n++) dumpWriter.Write("#");
                dumpWriter.WriteLine(" {0}", value.Value);
            }
            dumpWriter.WriteLine();

            dumpWriter.WriteLine("- Elements are not available in Definition.xml:\n====================================================");
            headerFmt = " {0,-15}| {1}";
            dumpWriter.WriteLine(headerFmt, "Path", "Count");
            dumpWriter.WriteLine(headerFmt
                , "---------------"
                , "------------------------------"
                );
            foreach (var value in none)
            {
                dumpWriter.Write(headerFmt, value.Key, string.Empty);
                for (int n = 1; n < value.Value * 29 / max_none_value; n++) dumpWriter.Write("#");
                dumpWriter.WriteLine(" {0}", value.Value);
            }
            dumpWriter.WriteLine();

            dumpWriter.Flush();

            return (int)(pr);
        }
    }
}
