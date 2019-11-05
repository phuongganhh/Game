
// This file was automatically generated by the Dapper.FastCRUD T4 Template
// Do not make changes directly to this file - edit the template configuration instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `connection`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.;Initial Catalog=mup;Integrated Security=True`
//     Include Views:          `True`

namespace Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Collections.Generic;

    /// <summary>
    /// A class which represents the Log table.
    /// </summary>
	[Table("Log")]
	public partial class Log
	{
		[Key]
	    public virtual string Id { get; set; }
	    public virtual string Message { get; set; }
	    public virtual string Type { get; set; }
	    public virtual string StackTrace { get; set; }
	    [ForeignKey("User")]
        public virtual long? UserId { get; set; }
	    public virtual string Param { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
		public virtual User User { get; set; }
	}

    /// <summary>
    /// A class which represents the Mail table.
    /// </summary>
	[Table("Mail")]
	public partial class Mail
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public virtual long Id { get; set; }
	    public virtual string Email { get; set; }
	    public virtual string Message { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    public virtual bool? Sent { get; set; }
	    public virtual DateTime? SentDate { get; set; }
	}

    /// <summary>
    /// A class which represents the Chat table.
    /// </summary>
	[Table("Chat")]
	public partial class Chat
	{
		[Key]
	    public virtual string Id { get; set; }
	    public virtual string Message { get; set; }
	    [ForeignKey("User")]
        public virtual long? SenderId { get; set; }
	    public virtual long? ReceiverId { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    public virtual bool? Deleted { get; set; }
		public virtual User User { get; set; }
	}

    /// <summary>
    /// A class which represents the NewsDetail table.
    /// </summary>
	[Table("NewsDetail")]
	public partial class NewsDetail
	{
		[Key]
	    public virtual long Id { get; set; }
	    public virtual string ContentHtml { get; set; }
	    public virtual string Content { get; set; }
		public virtual IEnumerable<News> News { get; set; }
	}

    /// <summary>
    /// A class which represents the News table.
    /// </summary>
	[Table("News")]
	public partial class News
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public virtual long Id { get; set; }
	    [ForeignKey("NewsGroup")]
        public virtual long NewsGroupId { get; set; }
	    [ForeignKey("NewsDetail")]
        public virtual long? NewsDetailId { get; set; }
	    [ForeignKey("User")]
        public virtual long? AuthorId { get; set; }
	    public virtual string Title { get; set; }
	    public virtual string Description { get; set; }
	    public virtual string Image { get; set; }
	    public virtual long? Views { get; set; }
	    public virtual bool? Deleted { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    public virtual DateTime? UpdatedDate { get; set; }
		public virtual NewsGroup NewsGroup { get; set; }
		public virtual NewsDetail NewsDetail { get; set; }
		public virtual User User { get; set; }
	}

    /// <summary>
    /// A class which represents the Language table.
    /// </summary>
	[Table("Language")]
	public partial class Language
	{
		[Key]
	    public virtual string Id { get; set; }
	    [ForeignKey("Region")]
        public virtual string RegionId { get; set; }
	    public virtual string Key { get; set; }
	    public virtual string Value { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    public virtual DateTime? UpdatedDate { get; set; }
	    public virtual long? CreaterId { get; set; }
	    public virtual long? UpdaterId { get; set; }
		public virtual Region Region { get; set; }
	}

    /// <summary>
    /// A class which represents the NewsGroup table.
    /// </summary>
	[Table("NewsGroup")]
	public partial class NewsGroup
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public virtual long Id { get; set; }
	    public virtual string Name { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    public virtual DateTime? UpdatedDate { get; set; }
	    public virtual bool? Deleted { get; set; }
		public virtual IEnumerable<News> News { get; set; }
	}

    /// <summary>
    /// A class which represents the Application table.
    /// </summary>
	[Table("Application")]
	public partial class Application
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public virtual long Id { get; set; }
	    [ForeignKey("ApplicationGroup")]
        public virtual long ApplicationGroupId { get; set; }
	    public virtual string Name { get; set; }
	    public virtual string Url { get; set; }
	    public virtual bool? Active { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    public virtual DateTime? UpdatedDate { get; set; }
	    public virtual long? ParentId { get; set; }
	    public virtual string Style { get; set; }
		public virtual ApplicationGroup ApplicationGroup { get; set; }
	}

    /// <summary>
    /// A class which represents the Region table.
    /// </summary>
	[Table("Region")]
	public partial class Region
	{
		[Key]
	    public virtual string Id { get; set; }
	    public virtual string Name { get; set; }
		public virtual IEnumerable<Language> Languages { get; set; }
	}

    /// <summary>
    /// A class which represents the Spin table.
    /// </summary>
	[Table("Spin")]
	public partial class Spin
	{
		[Key]
	    public virtual string Id { get; set; }
	    public virtual string Name { get; set; }
	    public virtual int Action { get; set; }
	    public virtual long? Money { get; set; }
	}

    /// <summary>
    /// A class which represents the ApplicationGroup table.
    /// </summary>
	[Table("ApplicationGroup")]
	public partial class ApplicationGroup
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public virtual long Id { get; set; }
	    public virtual string Name { get; set; }
		public virtual IEnumerable<Application> Applications { get; set; }
	}

    /// <summary>
    /// A class which represents the SpinLog table.
    /// </summary>
	[Table("SpinLog")]
	public partial class SpinLog
	{
		[Key]
	    public virtual string Id { get; set; }
	    public virtual long? ItemId { get; set; }
	    [ForeignKey("User")]
        public virtual long? UserId { get; set; }
	    public virtual long? MoneyBase { get; set; }
	    public virtual long? MoneyCurrent { get; set; }
	    public virtual long? ChangeCash { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
		public virtual User User { get; set; }
	}

    /// <summary>
    /// A class which represents the User table.
    /// </summary>
	[Table("User")]
	public partial class User
	{
		[Key]
	    public virtual long Id { get; set; }
	    [ForeignKey("FunctionGroup")]
        public virtual long FunctionGroupId { get; set; }
	    public virtual string Username { get; set; }
	    public virtual string Password { get; set; }
	    public virtual string CharacterName { get; set; }
	    public virtual long? Spin { get; set; }
	    [ForeignKey("File")]
        public virtual string FileImageId { get; set; }
	    public virtual long? Money { get; set; }
	    public virtual string Token { get; set; }
	    public virtual string Email { get; set; }
		public virtual FunctionGroup FunctionGroup { get; set; }
		public virtual File File { get; set; }
		public virtual IEnumerable<News> News { get; set; }
		public virtual IEnumerable<Chat> Chats { get; set; }
		public virtual IEnumerable<Log> Logs { get; set; }
		public virtual IEnumerable<Notify> Notifies { get; set; }
		public virtual IEnumerable<SpinLog> SpinLogs { get; set; }
		public virtual IEnumerable<File> Files { get; set; }
	}

    /// <summary>
    /// A class which represents the FunctionGroup table.
    /// </summary>
	[Table("FunctionGroup")]
	public partial class FunctionGroup
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public virtual long Id { get; set; }
	    [ForeignKey("FunctionItem")]
        public virtual long FunctionItemId { get; set; }
	    public virtual string Name { get; set; }
	    public virtual byte? Permission { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    public virtual DateTime? UpdatedDate { get; set; }
		public virtual FunctionItem FunctionItem { get; set; }
		public virtual IEnumerable<User> Users { get; set; }
	}

    /// <summary>
    /// A class which represents the FunctionItem table.
    /// </summary>
	[Table("FunctionItem")]
	public partial class FunctionItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public virtual long Id { get; set; }
	    public virtual string Name { get; set; }
	    public virtual string Area { get; set; }
	    public virtual string Controller { get; set; }
	    public virtual string Action { get; set; }
	    public virtual DateTime CreatedDate { get; set; }
	    public virtual bool Active { get; set; }
	    public virtual int Permission { get; set; }
		public virtual IEnumerable<FunctionGroup> FunctionGroups { get; set; }
	}

    /// <summary>
    /// A class which represents the Notify table.
    /// </summary>
	[Table("Notify")]
	public partial class Notify
	{
		[Key]
	    public virtual string Id { get; set; }
	    public virtual string Message { get; set; }
	    public virtual DateTime? StartDate { get; set; }
	    public virtual DateTime? EndDate { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    [ForeignKey("User")]
        public virtual long CreaterId { get; set; }
	    public virtual DateTime? UpdatedDate { get; set; }
	    [ForeignKey("User")]
        public virtual long UpdaterId { get; set; }
	    public virtual bool? Active { get; set; }
		public virtual User User { get; set; }
	}

    /// <summary>
    /// A class which represents the SpinItem table.
    /// </summary>
	[Table("SpinItem")]
	public partial class SpinItem
	{
		[Key]
	    public virtual long ItemId { get; set; }
	    public virtual string Name { get; set; }
	    public virtual long Count { get; set; }
	    public virtual string Image { get; set; }
	}

    /// <summary>
    /// A class which represents the File table.
    /// </summary>
	[Table("File")]
	public partial class File
	{
		[Key]
	    public virtual string Id { get; set; }
	    public virtual string Name { get; set; }
	    public virtual string PhysicalName { get; set; }
	    public virtual DateTime? CreatedDate { get; set; }
	    [ForeignKey("User")]
        public virtual long? CreaterId { get; set; }
		public virtual User User { get; set; }
		public virtual IEnumerable<User> Users { get; set; }
	}

    /// <summary>
    /// A class which represents the sysdiagrams table.
    /// </summary>
	[Table("sysdiagrams")]
	public partial class sysdiagram
	{
	    public virtual string name { get; set; }
	    public virtual int principal_id { get; set; }
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public virtual int diagram_id { get; set; }
	    public virtual int? version { get; set; }
	    public virtual byte[] definition { get; set; }
	}

}

