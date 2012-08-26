﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Simple.Data;
using Simple.Data.Extensions;

namespace Simple.Data.OData.Schema
{
    public class AssociationCollection : Collection<Association>
    {
        public AssociationCollection()
        {

        }

        public AssociationCollection(IEnumerable<Association> associations)
            : base(associations.ToList())
        {

        }

        public Association Find(string associationName)
        {
            var association = FindAssociationWithName(associationName);
            if (association == null) throw new UnresolvableObjectException(associationName);
            return association;
        }

        public bool Contains(string associationName)
        {
            return FindAssociationWithName(associationName) != null;
        }

        private Association FindAssociationWithName(string associationName)
        {
            associationName = associationName.Homogenize();
            return this
                .Where(c => c.HomogenizedActualName.Equals(associationName))
                .SingleOrDefault();
        }
    }
}