using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MapDrawers
{
	/// <summary>
	/// 地球を球とする。
	/// 極半径と赤道半径の平均を地球の半径とする。
	/// </summary>
	public static class MeterLatLon
	{
		private const double 地球_赤道面での直径_KM = 12756.274; // from Wiki
		private const double 地球_極半径_KM = 6356.752314; // from Wiki

		private const double Earth_R_Meter = 1000.0 * (地球_赤道面での直径_KM / 2.0 + 地球_極半径_KM) / 2.0;

		private const double Rate_DegreeToRad = Math.PI / 180.0;

		/// <summary>
		/// 緯度と長さ
		/// </summary>
		/// <returns>緯度1度の長さ(メートル)</returns>
		public static double MeterPerLat()
		{
			return Earth_R_Meter * 2.0 * Math.PI / 360.0;
		}

		/// <summary>
		/// 経度と長さ
		/// </summary>
		/// <param name="lat">緯度</param>
		/// <returns>指定緯度における経度1度の長さ(メートル)</returns>
		public static double MeterPerLon(double lat)
		{
			return Earth_R_Meter * Math.Cos(lat * Rate_DegreeToRad) * 2.0 * Math.PI / 360.0;
		}
	}
}
