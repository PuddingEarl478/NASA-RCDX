﻿/*     INFINITY CODE 2013-2019      */
/*   http://www.infinity-code.com   */

using UnityEngine;

namespace InfinityCode.RealWorldTerrain
{
    /// <summary>
    /// Class of POI instance.
    /// </summary>
    public class RealWorldTerrainPOIItem : MonoBehaviour
    {
        /// <summary>
        /// The title of the POI.
        /// </summary>
        public string title;

        /// <summary>
        /// Longitude.
        /// </summary>
        public float x;

        /// <summary>
        /// Latitude.
        /// </summary>
        public float y;

        /// <summary>
        /// Sets POI prefs.
        /// </summary>
        /// <param name="poi">Reference to POI.</param>
        public void SetPrefs(RealWorldTerrainPOI poi)
        {
            title = poi.title;
            x = poi.x;
            y = poi.y;
        }
    }
}
