using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI.Localization;

namespace Common
{
    public class HebrewRadGridLocalizationProvider : RadGridLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadGridStringId.FilterFunctionBetween: return "בין";
                case RadGridStringId.FilterFunctionContains: return "מכיל";
                case RadGridStringId.FilterFunctionDoesNotContain: return "לא מכיל";
                case RadGridStringId.FilterFunctionEndsWith: return "מסתיים ב";
                case RadGridStringId.FilterFunctionEqualTo: return "שווה ל";
                case RadGridStringId.FilterFunctionGreaterThan: return "גדול מ";
                case RadGridStringId.FilterFunctionGreaterThanOrEqualTo: return "גדול או שווה";
                case RadGridStringId.FilterFunctionIsEmpty: return "ריק";
                case RadGridStringId.FilterFunctionIsNull: return "ללא ערך";
                case RadGridStringId.FilterFunctionLessThan: return "קטן מ";
                case RadGridStringId.FilterFunctionLessThanOrEqualTo: return "קטן או שווה";
                case RadGridStringId.FilterFunctionNoFilter: return "ללא מסנן";
                case RadGridStringId.FilterFunctionNotBetween: return "לא בין";
                case RadGridStringId.FilterFunctionNotEqualTo: return "לא שווה ל";
                case RadGridStringId.FilterFunctionNotIsEmpty: return "לא ריק";
                case RadGridStringId.FilterFunctionNotIsNull: return "עם ערך";
                case RadGridStringId.FilterFunctionStartsWith: return "מתחיל ב";
                case RadGridStringId.FilterFunctionCustom: return "מותאם";

                case RadGridStringId.FilterOperatorBetween: return "בין";
                case RadGridStringId.FilterOperatorContains: return "מכיל";
                case RadGridStringId.FilterOperatorDoesNotContain: return "לא מכיל";
                case RadGridStringId.FilterOperatorEndsWith: return "מסתים ב";
                case RadGridStringId.FilterOperatorEqualTo: return "שווה";
                case RadGridStringId.FilterOperatorGreaterThan: return "גדול מ";
                case RadGridStringId.FilterOperatorGreaterThanOrEqualTo: return "גדול או שווה";
                case RadGridStringId.FilterOperatorIsEmpty: return "ריק";
                case RadGridStringId.FilterOperatorIsNull: return "ללא ערך";
                case RadGridStringId.FilterOperatorLessThan: return "קטן מ";
                case RadGridStringId.FilterOperatorLessThanOrEqualTo: return "קטן או שווה";
                case RadGridStringId.FilterOperatorNoFilter: return "ללא מסנן";
                case RadGridStringId.FilterOperatorNotBetween: return "לא בין";
                case RadGridStringId.FilterOperatorNotEqualTo: return "לא שווה";
                case RadGridStringId.FilterOperatorNotIsEmpty: return "לא ריק";
                case RadGridStringId.FilterOperatorNotIsNull: return "עם ערך";
                case RadGridStringId.FilterOperatorStartsWith: return "מתחיל ב";
                case RadGridStringId.FilterOperatorIsLike: return "כמו";
                case RadGridStringId.FilterOperatorNotIsLike: return "לא כמו";
                case RadGridStringId.FilterOperatorIsContainedIn: return "מוכל";
                case RadGridStringId.FilterOperatorNotIsContainedIn: return "לא מוכל";
                case RadGridStringId.FilterOperatorCustom: return "מותאם";

                case RadGridStringId.CustomFilterMenuItem: return "מותאם";
                case RadGridStringId.CustomFilterDialogCaption: return "עריכת מסנן";
                case RadGridStringId.CustomFilterDialogLabel: return "הראה שורות המקיימות";
                case RadGridStringId.CustomFilterDialogRbAnd: return "וגם";
                case RadGridStringId.CustomFilterDialogRbOr: return "או";
                case RadGridStringId.CustomFilterDialogBtnOk: return "אישור";
                case RadGridStringId.CustomFilterDialogBtnCancel: return "ביטול";
                case RadGridStringId.CustomFilterDialogCheckBoxNot: return "לא";
                case RadGridStringId.CustomFilterDialogTrue: return "True";
                case RadGridStringId.CustomFilterDialogFalse: return "False";

                case RadGridStringId.FilterMenuAvailableFilters: return "מסננים זמינים";
                case RadGridStringId.FilterMenuSearchBoxText: return "Search...";
                case RadGridStringId.FilterMenuClearFilters: return "ניקוי מסנן";
                case RadGridStringId.FilterMenuButtonOK: return "אישור";
                case RadGridStringId.FilterMenuButtonCancel: return "ביטול";
                case RadGridStringId.FilterMenuSelectionAll: return "הכל";
                case RadGridStringId.FilterMenuSelectionAllSearched: return "כל תוצאות החיפוש";
                case RadGridStringId.FilterMenuSelectionNull: return "ללא ערך";
                case RadGridStringId.FilterMenuSelectionNotNull: return "עם ערך";

                case RadGridStringId.FilterLogicalOperatorAnd: return "וגם";
                case RadGridStringId.FilterLogicalOperatorOr: return "או";
                case RadGridStringId.FilterCompositeNotOperator: return "לא";

                case RadGridStringId.DeleteRowMenuItem: return "מחק שורה";
                case RadGridStringId.SortAscendingMenuItem: return "מיון בסדר עולה";
                case RadGridStringId.SortDescendingMenuItem: return "מיון בסדר יורד";
                case RadGridStringId.ClearSortingMenuItem: return "ללא מיון";
                case RadGridStringId.ConditionalFormattingMenuItem: return "עיצוב מותנה";
                case RadGridStringId.GroupByThisColumnMenuItem: return "הקבצה לפי עמודה זו";
                case RadGridStringId.UngroupThisColumn: return "ביטול הקבצה זו";
                case RadGridStringId.ColumnChooserMenuItem: return "מנהל התצוגה";
                case RadGridStringId.HideMenuItem: return "הסתר עמודה";
                case RadGridStringId.UnpinMenuItem: return "שחרר קיבוע עמודה";
                case RadGridStringId.UnpinRowMenuItem: return "שחרר קיבוע שורה";
                case RadGridStringId.PinMenuItem: return "מצב קיבוע";
                case RadGridStringId.PinAtLeftMenuItem: return "קבע לימין";
                case RadGridStringId.PinAtRightMenuItem: return "קבע לשמאל";
                case RadGridStringId.PinAtBottomMenuItem: return "קבע למטה";
                case RadGridStringId.PinAtTopMenuItem: return "קבע למעלה";
                case RadGridStringId.BestFitMenuItem: return "התאמה מירבית";
                case RadGridStringId.PasteMenuItem: return "הדבק";
                case RadGridStringId.EditMenuItem: return "ערוך";
                case RadGridStringId.ClearValueMenuItem: return "נקה ערך";
                case RadGridStringId.CopyMenuItem: return "העתק";
                case RadGridStringId.AddNewRowString: return " ";
                case RadGridStringId.ConditionalFormattingCaption: return "מנהל העיצוב המותנה";
                case RadGridStringId.ConditionalFormattingLblColumn: return "עצב רק תאים עם:";
                case RadGridStringId.ConditionalFormattingLblName: return "שם חוק:";
                case RadGridStringId.ConditionalFormattingLblType: return "ערך התא:";
                case RadGridStringId.ConditionalFormattingLblValue1: return "ערך 1:";
                case RadGridStringId.ConditionalFormattingLblValue2: return "ערך 2:";
                case RadGridStringId.ConditionalFormattingGrpConditions: return "חוקים:";
                case RadGridStringId.ConditionalFormattingGrpProperties: return "מאפייני החוק:";
                case RadGridStringId.ConditionalFormattingChkApplyToRow: return "החל חוק זה על כל השורה";
                case RadGridStringId.ConditionalFormattingBtnAdd: return "חוק חדש";
                case RadGridStringId.ConditionalFormattingBtnRemove: return "הסר חוק נבחר";
                case RadGridStringId.ConditionalFormattingBtnOK: return "אישור";
                case RadGridStringId.ConditionalFormattingBtnCancel: return "ביטול";
                case RadGridStringId.ConditionalFormattingBtnApply: return "החל";
                case RadGridStringId.ConditionalFormattingRuleAppliesOn: return "חוק מוחל על:";
                case RadGridStringId.ConditionalFormattingChooseOne: return "[בחר אחד]";
                case RadGridStringId.ConditionalFormattingEqualsTo: return "שווה ל [ערך1]";
                case RadGridStringId.ConditionalFormattingIsNotEqualTo: return "לא שווה ל [ערך 1]";
                case RadGridStringId.ConditionalFormattingStartsWith: return "מתחיל ב [ערך 1]";
                case RadGridStringId.ConditionalFormattingEndsWith: return "מסתיים ב [ערך 1]";
                case RadGridStringId.ConditionalFormattingContains: return "מכיל [ערך 1]";
                case RadGridStringId.ConditionalFormattingDoesNotContain: return "לא מכיל [ערך 1]";
                case RadGridStringId.ConditionalFormattingIsGreaterThan: return "גדול מ [ערך 1]";
                case RadGridStringId.ConditionalFormattingIsGreaterThanOrEqual: return "גדול או שווה ל [ערך 1]";
                case RadGridStringId.ConditionalFormattingIsLessThan: return "קטן מ [ערך 1]";
                case RadGridStringId.ConditionalFormattingIsLessThanOrEqual: return "קטן או שווה ל [ערך 1]";
                case RadGridStringId.ConditionalFormattingIsBetween: return "בין [ערך 1] לבין [ערך 2]";
                case RadGridStringId.ConditionalFormattingIsNotBetween: return "לא בין [ערך 1] לבין [ערך 2]";

                case RadGridStringId.ColumnChooserFormCaption: return "מנהל התצוגה";
                case RadGridStringId.ColumnChooserFormMessage:
                    return "גרור כותרת מהטבלה לכאן,\n" + "כדי להסתירה מהתצוגה.";
                case RadGridStringId.GroupingPanelDefaultMessage: return "אזור גרירת כותרות לצורך הקבצה";
                case RadGridStringId.GroupingPanelHeader: return "הקבצה לפי:";
                case RadGridStringId.NoDataText: return "No data to display";
                case RadGridStringId.CompositeFilterFormErrorCaption: return "שגיאת מסנן";
            }

            return string.Empty;
        }

    }
}
