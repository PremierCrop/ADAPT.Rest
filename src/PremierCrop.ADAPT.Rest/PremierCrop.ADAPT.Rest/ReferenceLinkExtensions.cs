/*******************************************************************************
  * Copyright (c) 2018 Premier Crop Systems, LLC
  * All rights reserved. This program and the accompanying materials
  * are made available under the terms of the Eclipse Public License v1.0
  * which accompanies this distribution, and is available at
  * http://www.eclipse.org/legal/epl-v20.html
  *
  * Contributors:
  *    Keith Reimer - Initial version.
  *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremierCrop.ADAPT.Rest
{
    public static class ReferenceLinkExtensions
    {
        /// <summary>
        /// Gets the <see cref="ReferenceLink"/> with the rel value of "self" using the SingleOrDefault System.Linq method.
        /// </summary>
        /// <param name="links">The <see cref="ReferenceLink"/>s to query.</param>
        /// <returns>The <see cref="ReferenceLink"/>, if found, otherwise NULL.</returns>
        public static ReferenceLink SelfSingleOrDefault(this IEnumerable<ReferenceLink> links)
        {
            return links.SingleOrDefault(l => l.Rel == Relationships.Self);
        }

        /// <summary>
        /// Gets the <see cref="ReferenceLink"/> with the rel value of "self" using the Single System.Linq method.
        /// </summary>
        /// <param name="links">The <see cref="ReferenceLink"/>s to query.</param>
        /// <returns>The <see cref="ReferenceLink"/>.</returns>
        /// <exception cref="InvalidOperationException">If zero or more than one results are found.</exception>
        public static ReferenceLink SelfLinkSingle(this IEnumerable<ReferenceLink> links)
        {
            return links.Single(l => l.Rel == Relationships.Self);
        }

        /// <summary>
        /// Gets the <see cref="ReferenceLink"/>s with the rel value of type TLink using the Where System.Linq method.
        /// </summary>
        /// <param name="links">The <see cref="ReferenceLink"/>s to query.</param>
        /// <returns>The <see cref="ReferenceLink"/>s found with the matching rel.</returns>
        /// <seealso cref="Relationships">For details of how the rel values relate to the type TLink.</seealso>
        public static IReadOnlyCollection<ReferenceLink> ObjectRelWhere<TLink>(this IEnumerable<ReferenceLink> links)
        {
            return links.Where(l => l.Rel == typeof(TLink).ObjectRel()).ToArray();
        }

        /// <summary>
        /// Gets the <see cref="ReferenceLink"/> with the rel value of of type TLink using the Single System.Linq method.
        /// </summary>
        /// <param name="links">The <see cref="ReferenceLink"/>s to query.</param>
        /// <returns>The <see cref="ReferenceLink"/>.</returns>
        /// <exception cref="InvalidOperationException">If zero or more than one results are found.</exception>
        /// <seealso cref="Relationships">For details of how the rel values relate to the type TLink.</seealso>
        public static ReferenceLink ObjectRelSingle<TLink>(this IEnumerable<ReferenceLink> links)
        {
            return links.Single(l => l.Rel == typeof(TLink).ObjectRel());
        }

        /// <summary>
        /// Gets the <see cref="ReferenceLink"/> with the rel value of of type TLink using the SingleOrDefault System.Linq method.
        /// </summary>
        /// <param name="links">The <see cref="ReferenceLink"/>s to query.</param>
        /// <returns>The <see cref="ReferenceLink"/>, if found, otherwise NULL.</returns>
        /// <seealso cref="Relationships">For details of how the rel values relate to the type TLink.</seealso>
        public static ReferenceLink ObjectRelSingleOrDefault<TLink>(this IEnumerable<ReferenceLink> links)
        {
            return links.SingleOrDefault(l => l.Rel == typeof(TLink).ObjectRel());
        }

        /// <summary>
        /// Gets the <see cref="ReferenceLink"/> with the rel value of of type TLink pluralized using the Single System.Linq method.
        /// </summary>
        /// <param name="links">The <see cref="ReferenceLink"/>s to query.</param>
        /// <returns>The <see cref="ReferenceLink"/>.</returns>
        /// <exception cref="InvalidOperationException">If zero or more than one results are found.</exception>
        /// <seealso cref="Relationships">For details of how the rel values relate to the type TLink.</seealso>
        public static ReferenceLink ListRelSingle<TLink>(this IEnumerable<ReferenceLink> links)
        {
            return links.Single(l => l.Rel == typeof(TLink).ListRel());
        }

        /// <summary>
        /// Gets the <see cref="ReferenceLink"/> with the rel value of of type TLink pluralized using the SingleOrDefault System.Linq method.
        /// </summary>
        /// <param name="links">The <see cref="ReferenceLink"/>s to query.</param>
        /// <returns>The <see cref="ReferenceLink"/>, if found, otherwise NULL.</returns>
        /// <seealso cref="Relationships">For details of how the rel values relate to the type TLink.</seealso>
        public static ReferenceLink ListRelSingleOrDefault<TLink>(this IEnumerable<ReferenceLink> links)
        {
            return links.SingleOrDefault(l => l.Rel == typeof(TLink).ListRel());
        }
    }
}
