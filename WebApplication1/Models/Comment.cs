//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int Id { get; set; }
        public string Id_author { get; set; }
        public string Text { get; set; }
        public Nullable<int> Id_book { get; set; }
        public string Status { get; set; }
        public Nullable<int> Grade { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual reader reader { get; set; }
    }
}