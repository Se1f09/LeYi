//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homory.Model
{
    using System;
    using System.Collections.Generic;
    
    [EntityFramework.Audit.Audit]
    public partial class User
    {
        public User()
        {
            this.UserOnline = new HashSet<UserOnline>();
            this.UserRole = new HashSet<UserRole>();
            this.DepartmentUser = new HashSet<DepartmentUser>();
            this.Taught = new HashSet<Taught>();
            this.Resource = new HashSet<Resource>();
            this.UserCatalog = new HashSet<UserCatalog>();
            this.GroupUser = new HashSet<GroupUser>();
            this.Notice = new HashSet<Notice>();
            this.MeFavourite = new HashSet<UserFavourite>();
            this.FavouriteMe = new HashSet<UserFavourite>();
            this.ResourceComment = new HashSet<ResourceComment>();
            this.MediaNote = new HashSet<MediaNote>();
            this.ResourceAssess = new HashSet<ResourceAssess>();
            this.ResourceCommentTemp = new HashSet<ResourceCommentTemp>();
            this.ResourceLog = new HashSet<ResourceLog>();
        }
    
        public System.Guid Id { get; set; }
        public string Account { get; set; }
        public string RealName { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public System.Guid Stamp { get; set; }
        public string Password { get; set; }
        public string PasswordEx { get; set; }
        public string CryptoKey { get; set; }
        public string CryptoSalt { get; set; }
        public UserType Type { get; set; }
        public State State { get; set; }
        public int Ordinal { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<UserOnline> UserOnline { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<DepartmentUser> DepartmentUser { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Taught> Taught { get; set; }
        public virtual ICollection<Resource> Resource { get; set; }
        public virtual ICollection<UserCatalog> UserCatalog { get; set; }
        public virtual ICollection<GroupUser> GroupUser { get; set; }
        public virtual ICollection<Notice> Notice { get; set; }
        public virtual ICollection<UserFavourite> MeFavourite { get; set; }
        public virtual ICollection<UserFavourite> FavouriteMe { get; set; }
        public virtual ICollection<ResourceComment> ResourceComment { get; set; }
        public virtual ICollection<MediaNote> MediaNote { get; set; }
        public virtual ICollection<ResourceAssess> ResourceAssess { get; set; }
        public virtual ICollection<ResourceCommentTemp> ResourceCommentTemp { get; set; }
        public virtual ICollection<ResourceLog> ResourceLog { get; set; }
    }
}