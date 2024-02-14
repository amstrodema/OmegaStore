using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class dolly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Logo = table.Column<string>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    Group = table.Column<string>(type: "TEXT", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FeatureOptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FeatureID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureOptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Options = table.Column<string>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemFeatures",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderID = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderItemID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FeatureID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FeatureOption = table.Column<string>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFeatures", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CatID = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencySymbol = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    Image1 = table.Column<string>(type: "TEXT", nullable: false),
                    Image2 = table.Column<string>(type: "TEXT", nullable: false),
                    Image3 = table.Column<string>(type: "TEXT", nullable: false),
                    Image4 = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false),
                    Brief = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 14, scale: 2, nullable: false),
                    OldPrice = table.Column<decimal>(type: "TEXT", precision: 14, scale: 2, nullable: false),
                    IsRecent = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LoginMonitors",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimeLogged = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClientCode = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginMonitors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReceiverID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssociatedUserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Caption = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    Action = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DiscountCaption = table.Column<string>(type: "TEXT", nullable: false),
                    Discount = table.Column<decimal>(type: "TEXT", precision: 14, scale: 2, nullable: false),
                    No = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCoupon = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemName = table.Column<string>(type: "TEXT", nullable: false),
                    Qty = table.Column<decimal>(type: "TEXT", precision: 14, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 14, scale: 2, nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 14, scale: 2, nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Slides",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Caption = table.Column<string>(type: "TEXT", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    Action = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Logo = table.Column<string>(type: "TEXT", nullable: false),
                    LogoImage = table.Column<string>(type: "TEXT", nullable: false),
                    Tel = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Contact = table.Column<string>(type: "TEXT", nullable: false),
                    FacebookUrl = table.Column<string>(type: "TEXT", nullable: false),
                    InstagramUrl = table.Column<string>(type: "TEXT", nullable: false),
                    XUrl = table.Column<string>(type: "TEXT", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Brief = table.Column<string>(type: "TEXT", nullable: false),
                    FooterTag = table.Column<string>(type: "TEXT", nullable: false),
                    FooterDesc = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsBanned = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Trackings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trackings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleID = table.Column<Guid>(type: "TEXT", nullable: false),
                    MembershipID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CountryID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StoreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReferredBy = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Fname = table.Column<string>(type: "TEXT", nullable: false),
                    LName = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    Tel = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordVer = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Bio = table.Column<string>(type: "TEXT", nullable: false),
                    InstitutionLevel = table.Column<string>(type: "TEXT", nullable: false),
                    CV = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileImage = table.Column<string>(type: "TEXT", nullable: false),
                    EmailVerCode = table.Column<string>(type: "TEXT", nullable: false),
                    EmailVerExp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PasswordVerExp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SubExpiry = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsSubscribed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDev = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEmailVer = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBanned = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsOwner = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsInfluencer = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLearningCenter = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingDetails");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "FeatureOptions");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "ItemFeatures");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "LoginMonitors");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ShippingDetails");

            migrationBuilder.DropTable(
                name: "Slides");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Trackings");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
