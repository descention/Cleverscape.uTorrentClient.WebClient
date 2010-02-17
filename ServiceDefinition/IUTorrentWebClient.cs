using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace Cleverscape.UTorrentClient.WebClient.ServiceDefinition
{
    [ServiceContract]
    public interface IUTorrentWebClient
    {
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/?list=1&token={token}"
            )]
        TorrentsAndLabels GetAllTorrentsAndLabels(string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?list=1&cid={CacheID}&token={token}"
            )]
        UpdatedTorrentsAndLabels GetUpdatedTorrentsAndLabels(string CacheID, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=getsettings&token={token}"
            )]
        UTorrentSettings GetSettings(string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=setsetting&s={SettingName}&v={SettingValue}&token={token}"
            )]
        GenericResponse SetStringSetting(string SettingName, string SettingValue, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=setsetting&s={SettingName}&v={SettingValue}&token={token}"
            )]
        GenericResponse SetBooleanSetting(string SettingName, string SettingValue, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=setsetting&s={SettingName}&v={SettingValue}&token={token}"
            )]
        GenericResponse SetIntegerSetting(string SettingName, int SettingValue, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=getfiles&hash={TorrentHash}&token={token}"
            )]
        TorrentFiles GetFiles(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=getprops&hash={TorrentHash}&token={token}"
            )]
        TorrentProperties GetProperties(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/?action=setprops&hash={TorrentHash}&s={PropertyName}&v={PropertyValue}&token={token}"
            )]
        GenericResponse SetStringProperty(string TorrentHash, string PropertyName, string PropertyValue, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/?action=setprops&hash={TorrentHash}&s={PropertyName}&v={PropertyValue}&token={token}"
            )]
        GenericResponse SetIntegerProperty(string TorrentHash, string PropertyName, int PropertyValue, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/?action=setprops&hash={TorrentHash}&s={PropertyName}&v={PropertyValue}&token={token}"
            )]
        GenericResponse SetLongProperty(string TorrentHash, string PropertyName, long PropertyValue, string token);


        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/?action=start&hash={TorrentHash}&token={token}"
            )]
        GenericResponse StartTorrent(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=stop&hash={TorrentHash}&token={token}"
            )]
        GenericResponse StopTorrent(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=pause&hash={TorrentHash}&token={token}"
            )]
        GenericResponse PauseTorrent(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=forcestart&hash={TorrentHash}&token={token}"
            )]
        GenericResponse ForceStartTorrent(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=unpause&hash={TorrentHash}&token={token}"
            )]
        GenericResponse UnPauseTorrent(string TorrentHash, string token);
        
        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=recheck&hash={TorrentHash}&token={token}"
            )]
        GenericResponse RecheckTorrent(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=remove&hash={TorrentHash}&token={token}"
            )]
        GenericResponse RemoveTorrent(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=removedata&hash={TorrentHash}&token={token}"
            )]
        GenericResponse RemoveTorrentAndData(string TorrentHash, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=setprio&hash={TorrentHash}&p={Priority}&f={FileNumber}&token={token}"
            )]
        GenericResponse SetFilePriority(string TorrentHash, int FileNumber, int Priority, string token);

        [OperationContract]
        [WebGet(
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/?action=add-url&s={TorrentUrl}&token={token}"
            )]
        GenericResponse AddTorrentFromUrl(string TorrentUrl, string token);

        [OperationContract]
        [WebGet(
			BodyStyle = WebMessageBodyStyle.Bare,
			UriTemplate = "/token.html"
			)]
        Stream getToken();

    }

}
