﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Single_Capstone.Models
{
	[DataContract]
	public class DataPoint
	{
		public DataPoint(double y, string label)
		{
			this.Y = y;
			this.Label = label;
		}
		//Explicitly setting the name to be used while serializing to JSON. 
		[DataMember(Name = "label")]
		public string Label = null;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}
