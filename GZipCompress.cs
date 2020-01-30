using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastRankerTools {
	public class GZipCompress : HyoutaPluginBase.IProgram {
		public static int ExecuteCompress(List<string> args) {
			if (args.Count < 2) {
				Console.WriteLine("Usage: infile outfile");
				return -1;
			}

			Compress(new FileStream(args[0], FileMode.Open), args[1]);

			return 0;
		}

		public static void Compress(FileStream fs, string outpath) {
			fs.Position = 0;
			var outfile = new FileStream(outpath, System.IO.FileMode.Create);
			using (var compressed = new Ionic.Zlib.GZipStream(outfile, Ionic.Zlib.CompressionMode.Compress)) {
				byte[] buffer = new byte[4096];
				int numRead;
				while ((numRead = fs.Read(buffer, 0, buffer.Length)) != 0) {
					compressed.Write(buffer, 0, numRead);
				}
			}
			outfile.Close();
		}

		public int Execute(List<string> args) {
			return ExecuteCompress(args);
		}

		public List<string> Identifiers() {
			return new List<string>() { "Other.GZip.Compress" };
		}
	}
}
