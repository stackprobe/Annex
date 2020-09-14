using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{3c25110c-b124-48b6-a120-34790c128d41}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			Main3(
				@"C:\temp\waj_alstroemeria.png",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_alstroemeria_amana_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_alstroemeria_chiyuki_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_alstroemeria_tenka_1080x1920.jpg"
				);

			Main3(
				@"C:\temp\waj_hokagoclimaxgirls.png",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_hokagoclimaxgirls_rinze_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_hokagoclimaxgirls_chiyoko_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_hokagoclimaxgirls_kaho_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_hokagoclimaxgirls_juri_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_hokagoclimaxgirls_natsuha_1080x1920.jpg"
				);

			Main3(
				@"C:\temp\waj_illuminationstars.png",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_illuminationstars_hiori_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_illuminationstars_mano_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_illuminationstars_meguru_1080x1920.jpg"
				);

			Main3(
				@"C:\temp\waj_lantica.png",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_lantica_mamimi_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_lantica_kiriko_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_lantica_kogane_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_lantica_yuika_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_lantica_sakuya_1080x1920.jpg"
				);

			Main3(
				@"C:\temp\waj_noctchill.png",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_noctchill_hinana_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_noctchill_toru_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_noctchill_madoka_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_noctchill_koito_1080x1920.jpg"
				);

			Main3(
				@"C:\temp\waj_straylight.png",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_straylight_fuyuko_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_straylight_asahi_1080x1920.jpg",
				@"C:\etc\画像\壁紙\シャニマス公式\wp_straylight_mei_1080x1920.jpg"
				);
		}

		private const string IMG_TOOLS_EXE = @"C:\app\Kit\ImgTools\ImgTools.exe";

		private const int DEST_W = 1920;
		private const int DEST_H = 1080;

		// 0 ～ 100
		private const int EXTERIOR_BOKASHI_LEVEL = 10;
		//private const int EXTERIOR_BOKASHI_LEVEL = 30;

		// 0 ～ 100
		private const int EXTERIOR_AKARUSA_LEVEL = 70;
		//private const int EXTERIOR_AKARUSA_LEVEL = 50;

		private void Main3(string destImgFile, params string[] srcImgFiles)
		{
			int srcnum = srcImgFiles.Length;
			Canvas2[] srcImgs = new Canvas2[srcnum];

			for (int index = 0; index < srcnum; index++)
				srcImgs[index] = new Canvas2(srcImgFiles[index]);

			int src_w = srcImgs[0].GetWidth();
			int src_h = srcImgs[0].GetHeight();

			Canvas2 tiledImg = new Canvas2(src_w * srcnum, src_h);

			for (int index = 0; index < srcnum; index++)
				using (Graphics g = tiledImg.GetGraphics(false))
					g.DrawImage(srcImgs[index].GetImage(), src_w * index, 0);

			using (WorkingDir wd = new WorkingDir())
			{
				string tiledImgFile = wd.MakePath() + ".png";
				string interiorImgFile = wd.MakePath() + ".png";
				string exteriorImgFile = wd.MakePath() + ".png";

				tiledImg.Save(tiledImgFile);

				int interior_w;
				int interior_h;
				int exterior_w;
				int exterior_h;

				{
					int w_h = DoubleTools.ToInt(DEST_H * (double)tiledImg.GetWidth() / tiledImg.GetHeight()); // 高さを基準にした幅
					int h_w = DoubleTools.ToInt(DEST_W * (double)tiledImg.GetHeight() / tiledImg.GetWidth()); // 幅を基準にした高さ

					if (w_h < DEST_W)
					{
						interior_w = w_h;
						interior_h = DEST_H;
						exterior_w = DEST_W;
						exterior_h = h_w;
					}
					else
					{
						interior_w = DEST_W;
						interior_h = h_w;
						exterior_w = w_h;
						exterior_h = DEST_H;
					}
				}

				ProcessTools.Batch(new string[]
				{
					string.Format("\"{0}\" /RF \"{1}\" /WF \"{2}\" /E {3} {4}"
						, IMG_TOOLS_EXE
						, tiledImgFile
						, interiorImgFile
						, interior_w
						, interior_h
						),
					string.Format("\"{0}\" /RF \"{1}\" /WF \"{2}\" /E {3} {4} /C {5} {6} {7} {8} /BOKASHI 0 0 {9} {10} {11} 1 /DOTFLTR A R:{12} G:{12} B:{12} /2 \"{13}\" /PASTE {14} {15}"
						, IMG_TOOLS_EXE
						, tiledImgFile
						, exteriorImgFile
						, exterior_w
						, exterior_h
						, (exterior_w - DEST_W) / 2
						, (exterior_h - DEST_H) / 2
						, DEST_W
						, DEST_H
						, DEST_W
						, DEST_H
						, EXTERIOR_BOKASHI_LEVEL
						, EXTERIOR_AKARUSA_LEVEL
						, interiorImgFile
						, (DEST_W - interior_w) / 2
						, (DEST_H - interior_h) / 2
						),
				},
				wd.GetPath(".")
				);

				File.Copy(exteriorImgFile, destImgFile, true);
			}
		}
	}
}
