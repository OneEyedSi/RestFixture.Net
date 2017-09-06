﻿using System;
using System.Collections.Generic;
using System.Xml.XPath;
using restFixture.Net.Support;
using restFixture.Net.Tools;
using RestClient.Data;

/*  Copyright 2017 Simon Elms
 *
  *  This file is part of RestFixture.Net, a .NET port of the original Java 
 *  RestFixture written by Fabrizio Cannizzo and others.
 *
 *  RestFixture.Net is free software:
 *  You can redistribute it and/or modify it under the terms of the
 *  GNU Lesser General Public License as published by the Free Software Foundation,
 *  either version 3 of the License, or (at your option) any later version.
 *
 *  RestFixture.Net is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with RestFixture.Net.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace restFixture.Net.Handlers
{
	/// <summary>
	/// Handles let expressions on XML content, returning XML string rather than the
	/// string with the content within the tags.
	/// 
	/// @author smartrics
	/// 
	/// </summary>
	public class LetBodyXmlHandler : ILetHandler
	{

		public virtual string handle(IRunnerVariablesProvider variablesProvider, Config config, 
            RestResponse response, object expressionContext, string expression)
		{
			IDictionary<string, string> namespaceContext = (IDictionary<string, string>) expressionContext;
            XPathNavigator node = 
                XmlTools.extractXPath(namespaceContext, expression, response.Body);
			string val = XmlTools.xPathResultToXmlString(node);
			int pos = val.IndexOf("?>", StringComparison.Ordinal);
			if (pos >= 0)
			{
				val = val.Substring(pos + 2);
			}
			return val;
		}

	}

}