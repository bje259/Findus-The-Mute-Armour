using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.Root;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.CharacterInfo;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Inventory;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Inventory;
using Kingmaker.UI.MVVM._VM.Slots;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using Owlcat.Runtime.UI.Controls.Button;
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UniRx;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Findus.UI
{
	// Token: 0x02000A40 RID: 2624
	public class InventorySmartItemVM : CharInfoComponentVM
	{
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x001177CC File Offset: 0x001159CC
		public ItemEntity CurrentPolymorph
		{
			get
			{
				ItemEntity smartItem = this.m_SmartItem;
				if (smartItem == null)
				{
					return null;
				}
				ItemPolymorph.ItemPolymorphPart itemPolymorphPart = smartItem.Parts.Get<ItemPolymorph.ItemPolymorphPart>();
				if (itemPolymorphPart == null)
				{
					return null;
				}
				return itemPolymorphPart.CurrentPolymorph;
			}
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x001177F0 File Offset: 0x001159F0
		public InventorySmartItemVM(IReadOnlyReactiveProperty<UnitDescriptor> unit, ItemEntity smartItem, Action refreshAction) : base(unit)
		{
			this.m_SmartItem = smartItem;
			this.m_RefreshAction = refreshAction;
			this.CanStartDialog.Value = (this.HasDialog() && !RootUIContext.Instance.IsGlobalMap);
			this.CanPolymorph.Value = this.HasItems();
			this.Portrait = BlueprintRoot.Instance.SystemMechanics.FinneanUnit.PortraitSafe.HalfLengthPortrait;
			base.AddDisposable(this.SmartItemSlotVM = new SmartItemSlotVM(null, new Action<ItemEntity>(this.EquipItem), new Action<ItemEntity, ItemSlot>(this.EquipItemInSlot)));
			this.RefreshData();
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x001178C0 File Offset: 0x00115AC0
		public override void RefreshData()
		{
			base.RefreshData();
			if (this.m_SmartItem == null)
			{
				return;
			}
			this.PolymorphItems = this.GetPolymorphItems();
			ItemEntity item = null;
			if (this.CanPolymorph.Value)
			{
				ItemEntity current = this.CurrentPolymorph;
				this.ItemIndex = this.PolymorphItems.FindIndex(delegate (ItemEntity i)
				{
					BlueprintItem blueprint = i.Blueprint;
					//ItemEntity current = current;
					return blueprint == ((current != null) ? current.Blueprint : null);
				});
				if (this.ItemIndex > -1)
				{
					item = current;
				}
			}
			this.SmartItemSlotVM.SetItem(item);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x00117942 File Offset: 0x00115B42
		public bool HasDialog()
		{
			return this.m_SmartItem.Blueprint.ComponentsArray.Any((BlueprintComponent c) => c is ItemDialog);
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x00117978 File Offset: 0x00115B78
		public bool HasItems()
		{
			return this.m_SmartItem.Parts.Get<ItemPolymorph.ItemPolymorphPart>() != null;
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x00117990 File Offset: 0x00115B90
		public void StartDialog()
		{
			EventBus.RaiseEvent<INewServiceWindowUIHandler>(delegate (INewServiceWindowUIHandler h)
			{
				h.HandleCloseAll();
			}, true);
			BlueprintComponent blueprintComponent;
			ItemDialog itemDialog;
			if (this.m_SmartItem.Blueprint.ComponentsArray.TryFind((BlueprintComponent x) => x is ItemDialog, out blueprintComponent) && (itemDialog = (blueprintComponent as ItemDialog)) != null)
			{
				itemDialog.StartDialog();
			}
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x00117A0C File Offset: 0x00115C0C
		public List<ItemEntity> GetPolymorphItems()
		{
			List<ItemEntity> list = new List<ItemEntity>();
			ItemPolymorph.ItemPolymorphPart itemPolymorphPart = this.m_SmartItem.Parts.Get<ItemPolymorph.ItemPolymorphPart>();
			if (itemPolymorphPart == null)
			{
				return list;
			}
			HashSet<BlueprintItemReference> polymorphItems = itemPolymorphPart.PolymorphItems;
			IEnumerable<BlueprintItem> enumerable;
			if (polymorphItems == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = from i in polymorphItems
							 select i.Get() into i
							 where i != null
							 select i;
			}
			IEnumerable<BlueprintItem> enumerable2 = enumerable;
			if (enumerable2 == null)
			{
				return list;
			}
			list.AddRange(from i in enumerable2
						  select i.CreateEntity() into i
						  where i.CanBeEquippedBy(this.Unit.Value)
						  select i);
			foreach (ItemEntity itemEntity in list)
			{
				itemEntity.Identify();
			}
			return list;
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x00117B0C File Offset: 0x00115D0C
		public void SelectItem(int index)
		{
			this.SmartItemSlotVM.SetItem((index >= 0 && index < this.PolymorphItems.Count) ? this.PolymorphItems[index] : null);
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x00117B3A File Offset: 0x00115D3A
		public void SelectItem(ItemSlotVM itemSlotVM)
		{
			this.SmartItemSlotVM.SetItem((itemSlotVM != null) ? itemSlotVM.ItemEntity : null);
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x00117B53 File Offset: 0x00115D53
		public void EquipItem(ItemEntity item)
		{
			ItemPolymorph.ItemPolymorphPart itemPolymorphPart = this.m_SmartItem.Parts.Get<ItemPolymorph.ItemPolymorphPart>();
			if (itemPolymorphPart != null)
			{
				itemPolymorphPart.CreateAndEquipPolymorph(item, this.Unit.Value);
			}
			Action refreshAction = this.m_RefreshAction;
			if (refreshAction == null)
			{
				return;
			}
			refreshAction();
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x00117B91 File Offset: 0x00115D91
		public void EquipItemInSlot(ItemEntity item, ItemSlot itemSlot)
		{
			ItemPolymorph.ItemPolymorphPart itemPolymorphPart = this.m_SmartItem.Parts.Get<ItemPolymorph.ItemPolymorphPart>();
			if (itemPolymorphPart != null)
			{
				itemPolymorphPart.CreateAndEquipPolymorphInSlot(item, this.Unit.Value, itemSlot);
			}
			Action refreshAction = this.m_RefreshAction;
			if (refreshAction == null)
			{
				return;
			}
			refreshAction();
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x00117BD0 File Offset: 0x00115DD0
		public void ShowPolymorphItemsSelector()
		{
			base.AddDisposable(this.PolymorphItemsSelectorVM.Value = new InventoryEquipSelectorVM(new Action<ItemSlotVM>(this.SelectItem), new Action(this.HideEquipSelector), this.PolymorphItems));
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x00117C14 File Offset: 0x00115E14
		public void HideEquipSelector()
		{
			this.PolymorphItemsSelectorVM.Value.Dispose();
			this.PolymorphItemsSelectorVM.Value = null;
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public override void DisposeImplementation()
		{
		}

		// Token: 0x04002C55 RID: 11349
		public readonly ItemEntity m_SmartItem;

		// Token: 0x04002C56 RID: 11350
		public readonly Action m_RefreshAction;

		// Token: 0x04002C57 RID: 11351
		public Sprite Portrait;

		// Token: 0x04002C58 RID: 11352
		public SmartItemSlotVM SmartItemSlotVM;

		// Token: 0x04002C59 RID: 11353
		public BoolReactiveProperty CanStartDialog = new BoolReactiveProperty();

		// Token: 0x04002C5A RID: 11354
		public BoolReactiveProperty CanPolymorph = new BoolReactiveProperty();

		// Token: 0x04002C5B RID: 11355
		public List<ItemEntity> PolymorphItems;

		// Token: 0x04002C5C RID: 11356
		public int ItemIndex = -1;

		// Token: 0x04002C5D RID: 11357
		public readonly ReactiveProperty<InventoryEquipSelectorVM> PolymorphItemsSelectorVM = new ReactiveProperty<InventoryEquipSelectorVM>();
	}

	namespace Findus.UI
	{
		// Token: 0x02000CFE RID: 3326
		public class InventorySmartItemPCView : CharInfoComponentView<InventorySmartItemVM>
		{
			// Token: 0x0600565B RID: 22107 RVA: 0x001652C0 File Offset: 0x001634C0
			public override void Initialize()
			{
				if (this.m_IsInit)
				{
					return;
				}
				base.Initialize();
				this.m_TitleLabel.text = UIUtility.GetSaberBookFormat(UIStrings.Instance.CharacterSheet.SmartItemLabel, default(Color), 140, null, 0f);
				this.m_Dropdown.captionText.text = UIStrings.Instance.CharacterSheet.SmartItemPolymorph;
				this.m_StartDialogButtonLabel.text = UIStrings.Instance.CharacterSheet.SmartItemStartDialog;
				this.m_IsInit = true;
			}

			// Token: 0x0600565C RID: 22108 RVA: 0x00165360 File Offset: 0x00163560
			public override void BindViewImplementation()
			{
				base.BindViewImplementation();
				this.m_UnitPortrait.sprite = base.ViewModel.Portrait;
				this.m_SmartItemSlotView.Bind(base.ViewModel.SmartItemSlotVM);
				this.m_StartDialogButton.gameObject.SetActive(base.ViewModel.CanStartDialog.Value);
				base.AddDisposable(this.m_StartDialogButton.OnLeftClickAsObservable().Subscribe(new Action(base.ViewModel.StartDialog)));
				this.m_Dropdown.gameObject.SetActive(base.ViewModel.CanPolymorph.Value);
				base.AddDisposable(this.m_Dropdown.OnValueChangedAsObservable().Subscribe(delegate (int i)
				{
					base.ViewModel.SelectItem(i - 1);
				}));
				base.AddDisposable(base.ViewModel.SmartItemSlotVM.Item.Subscribe(delegate (ItemEntity _)
				{
					this.SetWielderLabel();
				}));
			}

			// Token: 0x0600565D RID: 22109 RVA: 0x00165450 File Offset: 0x00163650
			public override void RefreshView()
			{
				base.RefreshView();
				this.m_Dropdown.ClearOptions();
				List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>
			{
				new TMP_Dropdown.OptionData("-", null)
			};
				list.AddRange(from i in base.ViewModel.PolymorphItems
							  select new TMP_Dropdown.OptionData(i.Blueprint.NonIdentifiedName, i.Icon));
				this.m_Dropdown.AddOptions(list);
				this.m_Dropdown.value = base.ViewModel.ItemIndex + 1;
			}

			// Token: 0x0600565E RID: 22110 RVA: 0x001654E0 File Offset: 0x001636E0
			public void SetWielderLabel()
			{
				string canDoText = UIUtilityTexts.GetCanDoText(base.ViewModel.CurrentPolymorph ?? base.ViewModel.SmartItemSlotVM.ItemEntity, true);
				this.m_CurrentWielderLabel.text = canDoText;
				this.m_CurrentWielderLabel.gameObject.SetActive(!string.IsNullOrEmpty(canDoText));
			}

			// Token: 0x0400391B RID: 14619
			[SerializeField]
			public TextMeshProUGUI m_TitleLabel;

			// Token: 0x0400391C RID: 14620
			[SerializeField]
			public Image m_UnitPortrait;

			// Token: 0x0400391D RID: 14621
			[SerializeField]
			public OwlcatButton m_StartDialogButton;

			// Token: 0x0400391E RID: 14622
			[SerializeField]
			public TextMeshProUGUI m_StartDialogButtonLabel;

			// Token: 0x0400391F RID: 14623
			[SerializeField]
			public TMP_Dropdown m_Dropdown;

			// Token: 0x04003920 RID: 14624
			[SerializeField]
			public SmartItemSlotPCView m_SmartItemSlotView;

			// Token: 0x04003921 RID: 14625
			[SerializeField]
			public TextMeshProUGUI m_CurrentWielderLabel;

			// Token: 0x04003922 RID: 14626
			public bool m_IsInit;
		}
	}

}
