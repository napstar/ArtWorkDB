//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Artwork_App
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArtType
    {
        public ArtType()
        {
            this.Artworks = new HashSet<Artwork>();
        }
    
        public int ArtTypeID { get; set; }
        public string ArtType1 { get; set; }
    
        public virtual ICollection<Artwork> Artworks { get; set; }
    }
}