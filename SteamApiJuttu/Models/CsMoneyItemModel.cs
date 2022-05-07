namespace SteamApiJuttu.Models.CsMoney
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    public partial class CsMoneyItemModel
    {
        public static CsMoneyItemModel FromJson(string json) => JsonConvert.DeserializeObject<CsMoneyItemModel>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CsMoneyItemModel self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }


    public partial class CsMoneyItemModel
    {
        [JsonProperty("pageProps", NullValueHandling = NullValueHandling.Ignore)]
        public PageProps PageProps { get; set; }
        
    }

    public partial class Currency
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("sign", NullValueHandling = NullValueHandling.Ignore)]
        public string Sign { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public double? Value { get; set; }

        [JsonProperty("isSuffix", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSuffix { get; set; }
    }

    public partial class Namespaces
    {
        [JsonProperty("404", NullValueHandling = NullValueHandling.Ignore)]
        public The404 The404 { get; set; }

        [JsonProperty("breadcrumbs", NullValueHandling = NullValueHandling.Ignore)]
        public Breadcrumbs Breadcrumbs { get; set; }

        [JsonProperty("common", NullValueHandling = NullValueHandling.Ignore)]
        public NamespacesCommon Common { get; set; }

        [JsonProperty("header", NullValueHandling = NullValueHandling.Ignore)]
        public Header Header { get; set; }

        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, RecommendedStickers> Meta { get; set; }

        [JsonProperty("navigation", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Navigation { get; set; }

        [JsonProperty("sorting", NullValueHandling = NullValueHandling.Ignore)]
        public NamespacesSorting Sorting { get; set; }

        [JsonProperty("rarities", NullValueHandling = NullValueHandling.Ignore)]
        public Rarities Rarities { get; set; }

        [JsonProperty("sidebar", NullValueHandling = NullValueHandling.Ignore)]
        public Sidebar Sidebar { get; set; }

        [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
        public Types Types { get; set; }


        [JsonProperty("weapons", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Weapons { get; set; }
    }

    public partial class Breadcrumbs
    {
        [JsonProperty("allSkins", NullValueHandling = NullValueHandling.Ignore)]
        public string AllSkins { get; set; }

        [JsonProperty("skins", NullValueHandling = NullValueHandling.Ignore)]
        public string Skins { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("listAndSkins", NullValueHandling = NullValueHandling.Ignore)]
        public string ListAndSkins { get; set; }

        [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Link { get; set; }

        [JsonProperty("cases", NullValueHandling = NullValueHandling.Ignore)]
        public string Cases { get; set; }
    }

    public partial class NamespacesCommon
    {
        [JsonProperty("foundIn", NullValueHandling = NullValueHandling.Ignore)]
        public string FoundIn { get; set; }

        [JsonProperty("searchPlaceholder", NullValueHandling = NullValueHandling.Ignore)]
        public string SearchPlaceholder { get; set; }

        [JsonProperty("footer", NullValueHandling = NullValueHandling.Ignore)]
        public Footer Footer { get; set; }

        [JsonProperty("buttons", NullValueHandling = NullValueHandling.Ignore)]
        public Buttons Buttons { get; set; }

        [JsonProperty("skinsByQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public SkinsByQuantity SkinsByQuantity { get; set; }

        [JsonProperty("slider", NullValueHandling = NullValueHandling.Ignore)]
        public Slider Slider { get; set; }

        [JsonProperty("sorting", NullValueHandling = NullValueHandling.Ignore)]
        public CommonSorting Sorting { get; set; }

        [JsonProperty("priceChart", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChart PriceChart { get; set; }

        [JsonProperty("suggestionPopup", NullValueHandling = NullValueHandling.Ignore)]
        public SuggestionPopup SuggestionPopup { get; set; }

        [JsonProperty("recommendedStickers", NullValueHandling = NullValueHandling.Ignore)]
        public RecommendedStickers RecommendedStickers { get; set; }

        [JsonProperty("noPatternsError", NullValueHandling = NullValueHandling.Ignore)]
        public string NoPatternsError { get; set; }

        [JsonProperty("searchNoResult", NullValueHandling = NullValueHandling.Ignore)]
        public string SearchNoResult { get; set; }

        [JsonProperty("forEverySkin", NullValueHandling = NullValueHandling.Ignore)]
        public ForEverySkin ForEverySkin { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public string Total { get; set; }

        [JsonProperty("searchFor", NullValueHandling = NullValueHandling.Ignore)]
        public string SearchFor { get; set; }

        [JsonProperty("up", NullValueHandling = NullValueHandling.Ignore)]
        public string Up { get; set; }

        [JsonProperty("availableColor", NullValueHandling = NullValueHandling.Ignore)]
        public string AvailableColor { get; set; }

        [JsonProperty("designerPageSubtitle", NullValueHandling = NullValueHandling.Ignore)]
        public string DesignerPageSubtitle { get; set; }

        [JsonProperty("seriesPageSubtitle", NullValueHandling = NullValueHandling.Ignore)]
        public string SeriesPageSubtitle { get; set; }

        [JsonProperty("rareItemsPageSubtitle", NullValueHandling = NullValueHandling.Ignore)]
        public string RareItemsPageSubtitle { get; set; }

        [JsonProperty("cookieModal", NullValueHandling = NullValueHandling.Ignore)]
        public CookieModal CookieModal { get; set; }

        [JsonProperty("rareItemsCount", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> RareItemsCount { get; set; }

        [JsonProperty("settingsCopyModal", NullValueHandling = NullValueHandling.Ignore)]
        public SettingsCopyModal SettingsCopyModal { get; set; }

        [JsonProperty("currentPrice", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrentPrice { get; set; }

        [JsonProperty("showCards", NullValueHandling = NullValueHandling.Ignore)]
        public string ShowCards { get; set; }

        [JsonProperty("hideCards", NullValueHandling = NullValueHandling.Ignore)]
        public string HideCards { get; set; }

        [JsonProperty("cards", NullValueHandling = NullValueHandling.Ignore)]
        public Cards Cards { get; set; }

        [JsonProperty("skins", NullValueHandling = NullValueHandling.Ignore)]
        public Cards Skins { get; set; }

        [JsonProperty("other", NullValueHandling = NullValueHandling.Ignore)]
        public Other Other { get; set; }

        [JsonProperty("Popup", NullValueHandling = NullValueHandling.Ignore)]
        public Popup Popup { get; set; }
    }

    public partial class Buttons
    {
        [JsonProperty("showAll", NullValueHandling = NullValueHandling.Ignore)]
        public string ShowAll { get; set; }

        [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
        public string Types { get; set; }

        [JsonProperty("hide", NullValueHandling = NullValueHandling.Ignore)]
        public string Hide { get; set; }

        [JsonProperty("getIt", NullValueHandling = NullValueHandling.Ignore)]
        public string GetIt { get; set; }

        [JsonProperty("showValues", NullValueHandling = NullValueHandling.Ignore)]
        public string ShowValues { get; set; }

        [JsonProperty("hideValues", NullValueHandling = NullValueHandling.Ignore)]
        public string HideValues { get; set; }

        [JsonProperty("send", NullValueHandling = NullValueHandling.Ignore)]
        public string Send { get; set; }

        [JsonProperty("edit", NullValueHandling = NullValueHandling.Ignore)]
        public string Edit { get; set; }

        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public string From { get; set; }
    }

    public partial class Cards
    {
        [JsonProperty("show", NullValueHandling = NullValueHandling.Ignore)]
        public string Show { get; set; }

        [JsonProperty("hide", NullValueHandling = NullValueHandling.Ignore)]
        public string Hide { get; set; }
    }

    public partial class CookieModal
    {
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Text { get; set; }

        [JsonProperty("accept", NullValueHandling = NullValueHandling.Ignore)]
        public string Accept { get; set; }

        [JsonProperty("manage", NullValueHandling = NullValueHandling.Ignore)]
        public string Manage { get; set; }
    }

    public partial class Footer
    {
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("privacy", NullValueHandling = NullValueHandling.Ignore)]
        public string Privacy { get; set; }

        [JsonProperty("tos", NullValueHandling = NullValueHandling.Ignore)]
        public string Tos { get; set; }
    }

    public partial class ForEverySkin
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("li1", NullValueHandling = NullValueHandling.Ignore)]
        public string Li1 { get; set; }

        [JsonProperty("li2", NullValueHandling = NullValueHandling.Ignore)]
        public string Li2 { get; set; }

        [JsonProperty("li3", NullValueHandling = NullValueHandling.Ignore)]
        public string Li3 { get; set; }

        [JsonProperty("li4", NullValueHandling = NullValueHandling.Ignore)]
        public string Li4 { get; set; }
    }

    public partial class Other
    {
        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public string Series { get; set; }

        [JsonProperty("designers", NullValueHandling = NullValueHandling.Ignore)]
        public string Designers { get; set; }
    }

    public partial class Popup
    {
        [JsonProperty("New", NullValueHandling = NullValueHandling.Ignore)]
        public string New { get; set; }

        [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("Inspection", NullValueHandling = NullValueHandling.Ignore)]
        public string Inspection { get; set; }

        [JsonProperty("View", NullValueHandling = NullValueHandling.Ignore)]
        public string View { get; set; }

        [JsonProperty("Go", NullValueHandling = NullValueHandling.Ignore)]
        public string Go { get; set; }
    }

    public partial class PriceChart
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("skinText", NullValueHandling = NullValueHandling.Ignore)]
        public string SkinText { get; set; }

        [JsonProperty("placeholder", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Placeholder { get; set; }

        [JsonProperty("placeholderNoData", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> PlaceholderNoData { get; set; }
    }

    public partial class RecommendedStickers
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public partial class SettingsCopyModal
    {
        [JsonProperty("copy", NullValueHandling = NullValueHandling.Ignore)]
        public string Copy { get; set; }

        [JsonProperty("copied", NullValueHandling = NullValueHandling.Ignore)]
        public string Copied { get; set; }
    }

    public partial class SkinsByQuantity
    {
        [JsonProperty("1skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The1Skins { get; set; }

        [JsonProperty("2skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The2Skins { get; set; }

        [JsonProperty("3skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The3Skins { get; set; }

        [JsonProperty("4skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The4Skins { get; set; }

        [JsonProperty("5skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The5Skins { get; set; }

        [JsonProperty("6skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The6Skins { get; set; }

        [JsonProperty("7skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The7Skins { get; set; }

        [JsonProperty("8skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The8Skins { get; set; }

        [JsonProperty("9skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The9Skins { get; set; }

        [JsonProperty("0skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The0Skins { get; set; }

        [JsonProperty("11-20skins", NullValueHandling = NullValueHandling.Ignore)]
        public string The1120Skins { get; set; }
    }

    public partial class Slider
    {
        [JsonProperty("title1", NullValueHandling = NullValueHandling.Ignore)]
        public string Title1 { get; set; }

        [JsonProperty("title2", NullValueHandling = NullValueHandling.Ignore)]
        public string Title2 { get; set; }

        [JsonProperty("text1", NullValueHandling = NullValueHandling.Ignore)]
        public string Text1 { get; set; }

        [JsonProperty("text2", NullValueHandling = NullValueHandling.Ignore)]
        public string Text2 { get; set; }

        [JsonProperty("statistic1", NullValueHandling = NullValueHandling.Ignore)]
        public string Statistic1 { get; set; }

        [JsonProperty("statistic2", NullValueHandling = NullValueHandling.Ignore)]
        public string Statistic2 { get; set; }

        [JsonProperty("statistic3", NullValueHandling = NullValueHandling.Ignore)]
        public string Statistic3 { get; set; }
    }

    public partial class CommonSorting
    {
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }
    }

    public partial class SuggestionPopup
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("placeholder1", NullValueHandling = NullValueHandling.Ignore)]
        public string Placeholder1 { get; set; }

        [JsonProperty("placeholder2", NullValueHandling = NullValueHandling.Ignore)]
        public string Placeholder2 { get; set; }

        [JsonProperty("placeholder3", NullValueHandling = NullValueHandling.Ignore)]
        public string Placeholder3 { get; set; }

        [JsonProperty("error1", NullValueHandling = NullValueHandling.Ignore)]
        public string Error1 { get; set; }

        [JsonProperty("error2", NullValueHandling = NullValueHandling.Ignore)]
        public string Error2 { get; set; }

        [JsonProperty("error3", NullValueHandling = NullValueHandling.Ignore)]
        public string Error3 { get; set; }

        [JsonProperty("error4", NullValueHandling = NullValueHandling.Ignore)]
        public string Error4 { get; set; }

        [JsonProperty("error5", NullValueHandling = NullValueHandling.Ignore)]
        public string Error5 { get; set; }

        [JsonProperty("error6", NullValueHandling = NullValueHandling.Ignore)]
        public string Error6 { get; set; }

        [JsonProperty("successMessageTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string SuccessMessageTitle { get; set; }

        [JsonProperty("successMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string SuccessMessage { get; set; }

        [JsonProperty("send", NullValueHandling = NullValueHandling.Ignore)]
        public string Send { get; set; }
    }

    public partial class Header
    {
        [JsonProperty("investments-button", NullValueHandling = NullValueHandling.Ignore)]
        public The404 InvestmentsButton { get; set; }

        [JsonProperty("investments-modal", NullValueHandling = NullValueHandling.Ignore)]
        public InvestmentsModal InvestmentsModal { get; set; }

        [JsonProperty("preferences-dropdown", NullValueHandling = NullValueHandling.Ignore)]
        public PreferencesDropdown PreferencesDropdown { get; set; }

        [JsonProperty("preferences-modal", NullValueHandling = NullValueHandling.Ignore)]
        public The404 PreferencesModal { get; set; }

        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public User User { get; set; }
    }

    public partial class The404
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
    }

    public partial class InvestmentsModal
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("explore", NullValueHandling = NullValueHandling.Ignore)]
        public string Explore { get; set; }

        [JsonProperty("compare", NullValueHandling = NullValueHandling.Ignore)]
        public string Compare { get; set; }

        [JsonProperty("invest", NullValueHandling = NullValueHandling.Ignore)]
        public string Invest { get; set; }

        [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore)]
        public string Link { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
    }

    public partial class PreferencesDropdown
    {
        [JsonProperty("cancel", NullValueHandling = NullValueHandling.Ignore)]
        public string Cancel { get; set; }
    }

    public partial class User
    {
        [JsonProperty("login", NullValueHandling = NullValueHandling.Ignore)]
        public string Login { get; set; }

        [JsonProperty("logout", NullValueHandling = NullValueHandling.Ignore)]
        public string Logout { get; set; }
    }

    public partial class Rarities
    {
        [JsonProperty("rarity", NullValueHandling = NullValueHandling.Ignore)]
        public string Rarity { get; set; }

        [JsonProperty("Consumer Grade", NullValueHandling = NullValueHandling.Ignore)]
        public string ConsumerGrade { get; set; }

        [JsonProperty("Mil-Spec Grade", NullValueHandling = NullValueHandling.Ignore)]
        public string MilSpecGrade { get; set; }

        [JsonProperty("Mil-Spec", NullValueHandling = NullValueHandling.Ignore)]
        public string MilSpec { get; set; }

        [JsonProperty("Industrial Grade", NullValueHandling = NullValueHandling.Ignore)]
        public string IndustrialGrade { get; set; }

        [JsonProperty("Restricted", NullValueHandling = NullValueHandling.Ignore)]
        public string Restricted { get; set; }

        [JsonProperty("Classified", NullValueHandling = NullValueHandling.Ignore)]
        public string Classified { get; set; }

        [JsonProperty("Covert", NullValueHandling = NullValueHandling.Ignore)]
        public string Covert { get; set; }

        [JsonProperty("Base Grade", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseGrade { get; set; }

        [JsonProperty("High Grade", NullValueHandling = NullValueHandling.Ignore)]
        public string HighGrade { get; set; }

        [JsonProperty("Extraordinary", NullValueHandling = NullValueHandling.Ignore)]
        public string Extraordinary { get; set; }

        [JsonProperty("Remarkable", NullValueHandling = NullValueHandling.Ignore)]
        public string Remarkable { get; set; }

        [JsonProperty("Exotic", NullValueHandling = NullValueHandling.Ignore)]
        public string Exotic { get; set; }

        [JsonProperty("Contraband", NullValueHandling = NullValueHandling.Ignore)]
        public string Contraband { get; set; }

        [JsonProperty("Master", NullValueHandling = NullValueHandling.Ignore)]
        public string Master { get; set; }

        [JsonProperty("Superior", NullValueHandling = NullValueHandling.Ignore)]
        public string Superior { get; set; }

        [JsonProperty("Exceptional", NullValueHandling = NullValueHandling.Ignore)]
        public string Exceptional { get; set; }

        [JsonProperty("Distinguished", NullValueHandling = NullValueHandling.Ignore)]
        public string Distinguished { get; set; }

        [JsonProperty("Default", NullValueHandling = NullValueHandling.Ignore)]
        public string Default { get; set; }
    }

    public partial class Sidebar
    {
        [JsonProperty("skinClass", NullValueHandling = NullValueHandling.Ignore)]
        public string SkinClass { get; set; }

        [JsonProperty("stickerClass", NullValueHandling = NullValueHandling.Ignore)]
        public string StickerClass { get; set; }

        [JsonProperty("pinClass", NullValueHandling = NullValueHandling.Ignore)]
        public string PinClass { get; set; }

        [JsonProperty("graffitiClass", NullValueHandling = NullValueHandling.Ignore)]
        public string GraffitiClass { get; set; }

        [JsonProperty("musicKitClass", NullValueHandling = NullValueHandling.Ignore)]
        public string MusicKitClass { get; set; }

        [JsonProperty("wearLimits", NullValueHandling = NullValueHandling.Ignore)]
        public string WearLimits { get; set; }

        [JsonProperty("collection", NullValueHandling = NullValueHandling.Ignore)]
        public string Collection { get; set; }

        [JsonProperty("cases", NullValueHandling = NullValueHandling.Ignore)]
        public string Cases { get; set; }

        [JsonProperty("capsules", NullValueHandling = NullValueHandling.Ignore)]
        public string Capsules { get; set; }

        [JsonProperty("pack", NullValueHandling = NullValueHandling.Ignore)]
        public string Pack { get; set; }

        [JsonProperty("sprayControl", NullValueHandling = NullValueHandling.Ignore)]
        public string SprayControl { get; set; }

        [JsonProperty("rarities", NullValueHandling = NullValueHandling.Ignore)]
        public string Rarities { get; set; }

        [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
        public string Types { get; set; }

        [JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
        public string Stats { get; set; }

        [JsonProperty("pinsCapsules", NullValueHandling = NullValueHandling.Ignore)]
        public string PinsCapsules { get; set; }

        [JsonProperty("graffitiCapsules", NullValueHandling = NullValueHandling.Ignore)]
        public string GraffitiCapsules { get; set; }

        [JsonProperty("musicKitBoxes", NullValueHandling = NullValueHandling.Ignore)]
        public string MusicKitBoxes { get; set; }

        [JsonProperty("agentCollection", NullValueHandling = NullValueHandling.Ignore)]
        public string AgentCollection { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("stickerRarities", NullValueHandling = NullValueHandling.Ignore)]
        public string StickerRarities { get; set; }

        [JsonProperty("all", NullValueHandling = NullValueHandling.Ignore)]
        public string All { get; set; }
    }

    public partial class NamespacesSorting
    {
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }

        [JsonProperty("rarity", NullValueHandling = NullValueHandling.Ignore)]
        public string Rarity { get; set; }

        [JsonProperty("popularity", NullValueHandling = NullValueHandling.Ignore)]
        public string Popularity { get; set; }

        [JsonProperty("release_date", NullValueHandling = NullValueHandling.Ignore)]
        public string ReleaseDate { get; set; }
    }

    public partial class Types
    {
        [JsonProperty("★", NullValueHandling = NullValueHandling.Ignore)]
        public string Empty { get; set; }

        [JsonProperty("★ StatTrak™", NullValueHandling = NullValueHandling.Ignore)]
        public string TypesStatTrak { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("Normal", NullValueHandling = NullValueHandling.Ignore)]
        public string Normal { get; set; }

        [JsonProperty("Souvenir", NullValueHandling = NullValueHandling.Ignore)]
        public string Souvenir { get; set; }

        [JsonProperty("StatTrak™", NullValueHandling = NullValueHandling.Ignore)]
        public string StatTrak { get; set; }
    }


    public partial class PageProps
    {

        [JsonProperty("entry", NullValueHandling = NullValueHandling.Ignore)]
        public Entry Entry { get; set; }
    }

    public partial class ApolloState
    {
        [JsonProperty("ROOT_QUERY", NullValueHandling = NullValueHandling.Ignore)]
        public RootQuery RootQuery { get; set; }
    }

    public partial class RootQuery
    {
        [JsonProperty("__typename", NullValueHandling = NullValueHandling.Ignore)]
        public string Typename { get; set; }

        [JsonProperty("weapon({\"input\":{\"id\":\"ak-47\"}})", NullValueHandling = NullValueHandling.Ignore)]
        public Entry WeaponInputIdAk47 { get; set; }
    }

    public partial class Entry
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("hash_name", NullValueHandling = NullValueHandling.Ignore)]
        public string HashName { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("meta_title")]
        public object MetaTitle { get; set; }

        [JsonProperty("meta_description")]
        public object MetaDescription { get; set; }
        

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("hash_name", NullValueHandling = NullValueHandling.Ignore)]
        public string HashName { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Image { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("rarity", NullValueHandling = NullValueHandling.Ignore)]
        public string Rarity { get; set; }

        [JsonProperty("release_date", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReleaseDate { get; set; }

        [JsonProperty("slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }

        [JsonProperty("subtitle", NullValueHandling = NullValueHandling.Ignore)]
        public string Subtitle { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("collection", NullValueHandling = NullValueHandling.Ignore)]
        public List<Collection> Collection { get; set; }

        [JsonProperty("containers", NullValueHandling = NullValueHandling.Ignore)]
        public List<Collection> Containers { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public Price Price { get; set; }

        [JsonProperty("texts", NullValueHandling = NullValueHandling.Ignore)]
        public Texts Texts { get; set; }
    }

    public partial class Collection
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }
    }

    public partial class Price
    {
        [JsonProperty("common", NullValueHandling = NullValueHandling.Ignore)]
        public SpecialClass Common { get; set; }

        [JsonProperty("special")]
        public SpecialClass Special { get; set; }
    }

    public partial class SpecialClass
    {
        [JsonProperty("min", NullValueHandling = NullValueHandling.Ignore)]
        public double? Min { get; set; }

        [JsonProperty("max", NullValueHandling = NullValueHandling.Ignore)]
        public double? Max { get; set; }
    }

    public partial class Texts
    {
        [JsonProperty("appearance_history", NullValueHandling = NullValueHandling.Ignore)]
        public string AppearanceHistory { get; set; }
    }

    public partial class Stats
    {
        [JsonProperty("cooldown", NullValueHandling = NullValueHandling.Ignore)]
        public double? Cooldown { get; set; }

        [JsonProperty("damage_body", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> DamageBody { get; set; }

        [JsonProperty("damage_head", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> DamageHead { get; set; }

        [JsonProperty("damage_lmb")]
        public object DamageLmb { get; set; }

        [JsonProperty("damage_rmb")]
        public object DamageRmb { get; set; }

        [JsonProperty("kill_award", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> KillAward { get; set; }

        [JsonProperty("magazine_capacity", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> MagazineCapacity { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public long? Price { get; set; }

        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public long? Range { get; set; }

        [JsonProperty("rate_of_fire", NullValueHandling = NullValueHandling.Ignore)]
        public long? RateOfFire { get; set; }

        [JsonProperty("running_speed", NullValueHandling = NullValueHandling.Ignore)]
        public long? RunningSpeed { get; set; }

        [JsonProperty("team", NullValueHandling = NullValueHandling.Ignore)]
        public string Team { get; set; }
    }
}
