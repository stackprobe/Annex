using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			using (StreamReader reader = new StreamReader(@"C:\var2\res\国土数値情報\N01-07L-48-01.0a_GML\N01-07L-jgd.xml", Encoding.UTF8))
			{
				while (reader.ReadLine().ToString() != "<!-- 空間属性 -->")
				{ }

				for (; ; )
				{
					string line = reader.ReadLine().ToString();

					if (line == "<!-- 地物データ -->")
						break;

					if (Regex.IsMatch(line, "^<gml:Curve gml:id=\"cv_[0-9]+-[0-9]+\">$") == false) throw null;
					if (reader.ReadLine() != "\t<gml:segments>") throw null;
					if (reader.ReadLine() != "\t\t<gml:LineStringSegment>") throw null;
					if (reader.ReadLine() != "\t\t\t<gml:posList>") throw null;

					for (; ; )
					{
						line = reader.ReadLine().ToString();

						if (Regex.IsMatch(line, "^\t*[0-9]+\\.[0-9]+ [0-9]+\\.[0-9]+$") == false)
							break;
					}
					if (line != "\t\t\t</gml:posList>") throw null;
					if (reader.ReadLine() != "\t\t</gml:LineStringSegment>") throw null;
					if (reader.ReadLine() != "\t</gml:segments>") throw null;
					if (reader.ReadLine() != "</gml:Curve>") throw null;
				}
				for (; ; )
				{
					string line = reader.ReadLine().ToString();

					if (line == "</ksj:Dataset>")
						break;

					if (Regex.IsMatch(line, "^<ksj:Road gml:id=\"fi_[0-9]+\">$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t<ksj:location xlink:href=\"#cv_[0-9]+-[0-9]+\"/>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t<ksj:roadTypeCode>[0-9]+</ksj:roadTypeCode>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t<ksj:routeName>.+</ksj:routeName>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t<ksj:lineName>.*</ksj:lineName>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t<ksj:popularName>.*</ksj:popularName>$") == false) throw null;
					if (reader.ReadLine() != "</ksj:Road>") throw null;
				}
				if (reader.ReadLine() != null)
					throw null;
			}
			using (StreamReader reader = new StreamReader(@"C:\var2\res\国土数値情報\N03-190101_GML\N03-19_190101.xml", Encoding.UTF8))
			{
				while (reader.ReadLine().ToString() != "\t<!--  図形 --> ") // 図形の前に空白が2つ, 行末に空白が1つ
				{ }

				for (; ; )
				{
					string line = reader.ReadLine().ToString();

					if (line == "\t<!--  属性 --> ") // 属性の前に空白が2つ, 行末に空白が1つ
						break;

				curve:
					if (Regex.IsMatch(line, "^\t<gml:Curve gml:id=\"cv[0-9]+_[0-9]+\">$") == false) throw null;
					if (reader.ReadLine() != "\t\t<gml:segments>") throw null;
					if (reader.ReadLine() != "\t\t\t<gml:LineStringSegment>") throw null;
					if (reader.ReadLine() != "\t\t\t\t<gml:posList>") throw null;

					for (; ; )
					{
						line = reader.ReadLine().ToString();

						if (Regex.IsMatch(line, "^\t\t\t\t[0-9]+\\.[0-9]+ [0-9]+\\.[0-9]+$") == false)
							break;
					}
					if (line != "\t\t\t\t</gml:posList>") throw null;
					if (reader.ReadLine() != "\t\t\t</gml:LineStringSegment>") throw null;
					if (reader.ReadLine() != "\t\t</gml:segments>") throw null;
					if (reader.ReadLine() != "\t</gml:Curve>") throw null;

					// ----

					line = reader.ReadLine().ToString();

					if (line.StartsWith("\t<gml:Curve ")) // ? Surface スキップ
						goto curve;

					if (Regex.IsMatch(line, "^\t<gml:Surface gml:id=\"sf[0-9]+\">$") == false) throw null;
					if (reader.ReadLine() != "\t\t<gml:patches>") throw null;
					if (reader.ReadLine() != "\t\t\t<gml:PolygonPatch>") throw null;
					if (reader.ReadLine() != "\t\t\t\t<gml:exterior>") throw null;
					if (reader.ReadLine() != "\t\t\t\t\t<gml:Ring>") throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t\t\t\t\t\t<gml:curveMember xlink:href=\"#cv[0-9]+_[0-9]+\"/>$") == false) throw null;
					if (reader.ReadLine() != "\t\t\t\t\t</gml:Ring>") throw null;
					if (reader.ReadLine() != "\t\t\t\t</gml:exterior>") throw null;

					for (; ; )
					{
						line = reader.ReadLine().ToString();

						if (line != "\t\t\t\t<gml:interior>")
							break;

						if (reader.ReadLine() != "\t\t\t\t\t<gml:Ring>") throw null;
						if (Regex.IsMatch(reader.ReadLine(), "^\t\t\t\t\t\t<gml:curveMember xlink:href=\"#cv[0-9]+_[0-9]+\"/>$") == false) throw null;
						if (reader.ReadLine() != "\t\t\t\t\t</gml:Ring>") throw null;
						if (reader.ReadLine() != "\t\t\t\t</gml:interior>") throw null;
					}
					if (line != "\t\t\t</gml:PolygonPatch>") throw null;
					if (reader.ReadLine() != "\t\t</gml:patches>") throw null;
					if (reader.ReadLine() != "\t</gml:Surface>") throw null;
				}
				for (; ; )
				{
					string line = reader.ReadLine().ToString();

					if (line == "</ksj:Dataset>")
						break;

					if (Regex.IsMatch(line, "^\t<ksj:AdministrativeBoundary gml:id=\"gy[0-9]+\">$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t\t<ksj:bounds xlink:href=\"#sf[0-9]+\"/>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t\t<ksj:prefectureName>.+</ksj:prefectureName>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t\t<ksj:subPrefectureName>.*</ksj:subPrefectureName>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t\t<ksj:countyName>.*</ksj:countyName>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t\t<ksj:cityName>.*</ksj:cityName>$") == false) throw null;
					if (Regex.IsMatch(reader.ReadLine(), "^\t\t<ksj:administrativeAreaCode codeSpace=\"AdministrativeAreaCode.xml\">[0-9]*</ksj:administrativeAreaCode>$") == false) throw null;
					if (reader.ReadLine() != "\t</ksj:AdministrativeBoundary>") throw null;
				}
				if (reader.ReadLine() != null)
					throw null;
			}
		}
	}
}
