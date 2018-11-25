## HW8 Note

### Coding, cretae Model for the four tables

要求里有四个表格，所以我打算建立四个Model,一切都很顺利，并没有太大的困难，但当我新建Controller with views and model 的时候发生错误，所以我觉得借鉴下别人已完成的意见，果然发生了一点小问题。
关于relation和foreign key的问题，Item中有使用到Seller,所以在Seller 的Model编制中需要把Item编入Model的构造参数中，就像这样：
```
   public Seller()
        {
            Items = new HashSet<Item>();   // 实例化Item
        }
      
        public int SellerID { get; set; }

        [Key]
        [Required, Display(Name = "Seller Name:")]
        [RegularExpression("^[a-zA-Z ]{1,20}$", ErrorMessage = "Please Input Your Legal Name")]
        public string SellerName { get; set; }

        public virtual ICollection<Item> Items { get; set; } //这里要加入Item，然后回到上面实例化
```
另一种情况比如Bid中只有使用别人的，自己没有被别人使用的，就单纯的加入而不用实例化，就像这样：
```
    public class Bid
    {

        [Key]
        public int BidID { get; set; }

        [Required, Display(Name = "Price:")]
        [RegularExpression("^[1-9]{1,20}$")]
        public string Price { get; set; }

        [Required, Display(Name = "Timestamp:")]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [Required, Display(Name = "Buyer Name:")]
        [RegularExpression("^[a-zA-Z ]{1,20}$", ErrorMessage = "Please Input Your Legal Name")]
        public string BuyerName { get; set; }

        public virtual Buyer Buyer { get; set; }

        public virtual Item Item { get; set; }
    }
```
在写database script的时候完全是模仿HW6里面的来写的，尤其是foreign key的限制，然后我发现别人出于方便就把Buyer和Seller的主键设为了Name，行吧无所谓，我也改了，因为在其他表格中要显示的就是name，建立表格大概就是这样写的的：
```
-- Seller table
CREATE TABLE [dbo].[Sellers]
(
	[SellerID]		INT IDENTITY (1,1)	NOT NULL,
	[SellerName]	NVARCHAR(64)		NOT NULL,
	CONSTRAINT [PK_dbo.Seller] PRIMARY KEY CLUSTERED ([SellerName])
);

-- Item table
CREATE TABLE [dbo].[Items]
(
	[ItemID]		INT IDENTITY (1,1)	NOT NULL,
	[ItemName]	NVARCHAR(64)		NOT NULL,
	[Description]	NVARCHAR(64)		NOT NULL,
	[SellerName]		NVARCHAR(64)		NOT NULL,
	CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED ([ItemID]),
	CONSTRAINT [FK_dbo.Sellers] FOREIGN KEY ([SellerName]) REFERENCES [dbo].[Sellers] ([SellerName]),
);
```