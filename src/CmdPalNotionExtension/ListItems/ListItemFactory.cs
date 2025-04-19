
using CmdPalNotionExtension.Authentication;
using CmdPalNotionExtension.Commands;
using CmdPalNotionExtension.Notion;
using NotionPage = CmdPalNotionExtension.Notion.Models.Page;

namespace CmdPalNotionExtension.ListItems;

internal sealed partial class ListItemFactory
{
  private readonly CommandFactory _commandFactory;
  private readonly TokenService _tokenService;
  private readonly NotionDataProvider _dataProvider;

  public ListItemFactory(CommandFactory commandFactory, TokenService tokenService, NotionDataProvider dataProvider)
  {
    _commandFactory = commandFactory;
    _tokenService = tokenService;
    _dataProvider = dataProvider;
  }

  internal NotionPageListItem Create(NotionPage notionPage)
  {
    var linkCommand = new LinkCommand(notionPage);
    var pageListItem = new NotionPageListItem(notionPage, linkCommand);
    return pageListItem;

    //var linkCommand = new LinkCommand(anime);
    //var animeListItem = new AnimeListItem(anime, linkCommand);

    //var form = new AnimeUpdateForm(anime, _dataUpdater);
    //var contentPage = new AnimeContentPage(anime, form, _tokenService);

    //var pageCommands = new List<CommandContextItem>()
    //    {
    //        new(linkCommand),
    //    };

    //var moreCommands = new List<CommandContextItem>()
    //    {
    //        new(contentPage)
    //    };

    //if (_tokenService.IsLoggedIn())
    //{
    //  var updateAnimeStatusCommand = _commandFactory.CreateUpdateAnimeStatusCommand(anime, AnimeStatusType.PlanToWatch);
    //  moreCommands.Add(new(updateAnimeStatusCommand));
    //  pageCommands.Add(new(updateAnimeStatusCommand));

    //  updateAnimeStatusCommand.AnimeStatusUpdated += contentPage.OnAnimeStatusUpdated;

    //  var deleteCommand = _commandFactory.CreateDeleteAnimeCommand(anime);
    //  deleteCommand.AnimeDeleted += animeListItem.OnAnimeStatusUpdated;
    //  deleteCommand.AnimeDeleted += contentPage.OnAnimeStatusUpdated;

    //  moreCommands.Add(new(deleteCommand));
    //  pageCommands.Add(new(deleteCommand));
    //}

    //animeListItem.MoreCommands = moreCommands.ToArray();
    //contentPage.Commands = pageCommands.ToArray();
    
  }
}