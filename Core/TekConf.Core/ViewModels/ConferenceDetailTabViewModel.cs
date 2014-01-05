using System;
using Cirrious.MvvmCross.ViewModels;
using TekConf.Core.ViewModels;
using Cirrious.CrossCore;

namespace TekConf.Core
{
	public class ConferenceDetailTabViewModel : MvxViewModel
	{
		public ConferenceDetailTabViewModel (
			ConferenceDetailViewModel detailVm,
			ConferenceSessionsViewModel sessionsVm,
			ConferenceLocationViewModel locationVm)
		{
			DetailVm = detailVm;
			SessionsVm = sessionsVm;
			LocationVm = locationVm;
			FavoritesVm = new ConferenceFavoriteSessionsViewModel (sessionsVm);
		}

		public string Slug {get;set;}
		public ConferenceDetailViewModel DetailVm { get; set; }
		public ConferenceSessionsViewModel SessionsVm { get; set; }
		public ConferenceLocationViewModel LocationVm { get; set; }
		public ConferenceFavoriteSessionsViewModel FavoritesVm { get; set; }

		public void Init(string slug)
		{
			Slug = slug;
		}
	}
}

