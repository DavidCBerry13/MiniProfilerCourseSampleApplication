/**
 * ag-grid - Advanced Data Grid / Data Table supporting Javascript / React / AngularJS / Web Components
 * @version v18.0.1
 * @link http://www.ag-grid.com/
 * @license MIT
 */
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var PropertyKeys = (function () {
    function PropertyKeys() {
    }
    PropertyKeys.STRING_PROPERTIES = [
        'sortingOrder', 'rowClass', 'rowSelection', 'overlayLoadingTemplate',
        'overlayNoRowsTemplate', 'quickFilterText', 'rowModelType',
        'editType', 'domLayout', 'clipboardDeliminator', 'rowGroupPanelShow',
        'multiSortKey', 'pivotColumnGroupTotals', 'pivotRowTotals'
    ];
    PropertyKeys.OBJECT_PROPERTIES = [
        'components', 'frameworkComponents', 'rowStyle', 'context', 'autoGroupColumnDef', 'groupColumnDef', 'localeText',
        'icons', 'datasource', 'serverSideDatasource', 'viewportDatasource', 'groupRowRendererParams', 'aggFuncs',
        'fullWidthCellRendererParams', 'defaultColGroupDef', 'defaultColDef', 'defaultExportParams', 'columnTypes',
        'rowClassRules', 'detailGridOptions', 'detailCellRendererParams', 'loadingOverlayComponentParams',
        'noRowsOverlayComponentParams', 'popupParent'
        //,'cellRenderers','cellEditors'
    ];
    PropertyKeys.ARRAY_PROPERTIES = [
        'slaveGrids', 'alignedGrids', 'rowData',
        'columnDefs', 'excelStyles', 'pinnedTopRowData', 'pinnedBottomRowData'
        // deprecated
    ];
    PropertyKeys.NUMBER_PROPERTIES = [
        'rowHeight', 'detailRowHeight', 'rowBuffer', 'colWidth', 'headerHeight', 'groupHeaderHeight',
        'floatingFiltersHeight', 'pivotHeaderHeight', 'pivotGroupHeaderHeight', 'groupDefaultExpanded',
        'minColWidth', 'maxColWidth', 'viewportRowModelPageSize', 'viewportRowModelBufferSize',
        'layoutInterval', 'autoSizePadding', 'maxBlocksInCache', 'maxConcurrentDatasourceRequests',
        'cacheOverflowSize', 'paginationPageSize', 'cacheBlockSize', 'infiniteInitialRowCount',
        'scrollbarWidth', 'paginationStartPage', 'infiniteBlockSize', 'batchUpdateWaitMillis'
    ];
    PropertyKeys.BOOLEAN_PROPERTIES = [
        'toolPanelSuppressRowGroups', 'toolPanelSuppressValues', 'toolPanelSuppressPivots', 'toolPanelSuppressPivotMode',
        'toolPanelSuppressSideButtons', 'toolPanelSuppressColumnFilter', 'toolPanelSuppressColumnSelectAll',
        'toolPanelSuppressColumnExpandAll', 'suppressMakeColumnVisibleAfterUnGroup',
        'suppressRowClickSelection', 'suppressCellSelection', 'suppressHorizontalScroll', 'debug',
        'enableColResize', 'enableCellExpressions', 'enableSorting', 'enableServerSideSorting',
        'enableFilter', 'enableServerSideFilter', 'angularCompileRows', 'angularCompileFilters',
        'angularCompileHeaders', 'groupSuppressAutoColumn', 'groupSelectsChildren',
        'groupIncludeFooter', 'groupIncludeTotalFooter', 'groupUseEntireRow', 'groupSuppressRow', 'groupSuppressBlankHeader',
        'forPrint', 'suppressMenuHide', 'rowDeselection', 'unSortIcon', 'suppressMultiSort',
        'singleClickEdit', 'suppressLoadingOverlay', 'suppressNoRowsOverlay', 'suppressAutoSize',
        'suppressParentsInRowNodes', 'showToolPanel', 'suppressColumnMoveAnimation', 'suppressMovableColumns',
        'suppressFieldDotNotation', 'enableRangeSelection',
        'pivotPanelShow', 'suppressTouch', 'suppressAsyncEvents', 'allowContextMenuWithControlKey',
        'suppressContextMenu', 'suppressMenuFilterPanel', 'suppressMenuMainPanel', 'suppressMenuColumnPanel',
        'enableStatusBar', 'alwaysShowStatusBar', 'rememberGroupStateWhenNewData', 'enableCellChangeFlash', 'suppressDragLeaveHidesColumns',
        'suppressMiddleClickScrolls', 'suppressPreventDefaultOnMouseWheel', 'suppressUseColIdForGroups',
        'suppressCopyRowsToClipboard', 'pivotMode', 'suppressAggFuncInHeader', 'suppressColumnVirtualisation', 'suppressAggAtRootLevel',
        'suppressFocusAfterRefresh', 'functionsPassive', 'functionsReadOnly',
        'animateRows', 'groupSelectsFiltered', 'groupRemoveSingleChildren', 'groupRemoveLowestSingleChildren',
        'enableRtl', 'suppressClickEdit', 'rowDragManaged', 'suppressRowDrag',
        'enableGroupEdit', 'embedFullWidthRows', 'suppressTabbing', 'suppressPaginationPanel', 'floatingFilter',
        'groupHideOpenParents', 'groupMultiAutoColumn', 'pagination', 'stopEditingWhenGridLosesFocus',
        'paginationAutoPageSize', 'suppressScrollOnNewData', 'purgeClosedRowNodes', 'cacheQuickFilter',
        'deltaRowDataMode', 'ensureDomOrder', 'accentedSort', 'pivotTotals', 'suppressChangeDetection',
        'valueCache', 'valueCacheNeverExpires', 'aggregateOnlyChangedColumns', 'suppressAnimationFrame',
        'suppressExcelExport', 'suppressCsvExport', 'treeData', 'masterDetail', 'suppressMultiRangeSelection',
        'enterMovesDownAfterEdit', 'enterMovesDown', 'suppressPropertyNamesCheck', 'rowMultiSelectWithClick',
        'contractColumnSelection', 'suppressEnterpriseResetOnNewColumns', 'enableOldSetFilterModel',
        'suppressRowHoverHighlight', 'gridAutoHeight', 'suppressRowTransform', 'suppressClipboardPaste'
    ];
    PropertyKeys.FUNCTION_PROPERTIES = ['localeTextFunc', 'groupRowInnerRenderer', 'groupRowInnerRendererFramework',
        'dateComponent', 'dateComponentFramework', 'groupRowRenderer', 'groupRowRendererFramework', 'isExternalFilterPresent',
        'getRowHeight', 'doesExternalFilterPass', 'getRowClass', 'getRowStyle', 'getRowClassRules',
        'traverseNode', 'getContextMenuItems', 'getMainMenuItems', 'processRowPostCreate', 'processCellForClipboard',
        'getNodeChildDetails', 'groupRowAggNodes', 'getRowNodeId', 'isFullWidthCell', 'fullWidthCellRenderer',
        'fullWidthCellRendererFramework', 'doesDataFlower', 'processSecondaryColDef', 'processSecondaryColGroupDef',
        'getBusinessKeyForNode', 'sendToClipboard', 'navigateToNextCell', 'tabToNextCell', 'getDetailRowData',
        'processCellFromClipboard', 'getDocument', 'postProcessPopup', 'getChildCount', 'getDataPath', 'loadingOverlayComponent',
        'loadingOverlayComponentFramework', 'noRowsOverlayComponent', 'noRowsOverlayComponentFramework', 'detailCellRenderer',
        'detailCellRendererFramework', 'onGridReady', 'defaultGroupSortComparator', 'isRowMaster', 'isRowSelectable', 'postSort',
        'processHeaderForClipboard', 'paginationNumberFormatter', 'processDataFromClipboard'];
    PropertyKeys.ALL_PROPERTIES = PropertyKeys.ARRAY_PROPERTIES
        .concat(PropertyKeys.OBJECT_PROPERTIES)
        .concat(PropertyKeys.STRING_PROPERTIES)
        .concat(PropertyKeys.NUMBER_PROPERTIES)
        .concat(PropertyKeys.FUNCTION_PROPERTIES)
        .concat(PropertyKeys.BOOLEAN_PROPERTIES);
    return PropertyKeys;
}());
exports.PropertyKeys = PropertyKeys;
