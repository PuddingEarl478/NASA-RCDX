﻿/*     INFINITY CODE 2013-2019      */
/*   http://www.infinity-code.com   */

using System;
using System.Xml;
using UnityEngine;

namespace InfinityCode.RealWorldTerrain
{
    /// <summary>
    /// Class points of interest.
    /// </summary>
    [Serializable]
    public class RealWorldTerrainPOI
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

        public RealWorldTerrainPOI()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">POI title.</param>
        /// <param name="x">Longitude.</param>
        /// <param name="y">Latitude.</param>
        public RealWorldTerrainPOI(string title, float x, float y)
        {
            this.title = title;
            this.x = x;
            this.y = y;
        }

        public RealWorldTerrainPOI(XmlNode node)
        {
            try
            {
                x = RealWorldTerrainXMLExt.GetAttribute<float>(node, "x");
                y = RealWorldTerrainXMLExt.GetAttribute<float>(node, "y");
                title = node.InnerText;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                throw;
            }
        }
    }
}