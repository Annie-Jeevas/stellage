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
    
    public partial class reader
    {
        public reader()
        {
            this.Comments = new HashSet<Comment>();
            this.Requests = new HashSet<Request>();
            this.Books = new HashSet<Book>();
        }
    
        public string Id_reader { get; set; }
        public Nullable<byte> InSystem { get; set; }
        public Nullable<byte> Ban { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
