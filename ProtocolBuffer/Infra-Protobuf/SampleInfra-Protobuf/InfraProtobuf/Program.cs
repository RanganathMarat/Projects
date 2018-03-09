using System;

namespace Philips.PmsMR.Protobuf.ProtoGenerator
{
    class Program
    {
        static int Main ( string[] args )
        {
            var protoGen = new ProtoGenerator();
            if (protoGen.ParserArguments(args))
            {
                Console.WriteLine("Proto files generated successfully");
            }
            else
            {
                Console.WriteLine ( "Proto files generation failed" );
                return 1;
            }
            return 0;
        }
    }
}
