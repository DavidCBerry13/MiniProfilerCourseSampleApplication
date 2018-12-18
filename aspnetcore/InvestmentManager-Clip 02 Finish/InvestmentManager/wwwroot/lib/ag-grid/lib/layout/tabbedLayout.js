/**
 * ag-grid - Advanced Data Grid / Data Table supporting Javascript / React / AngularJS / Web Components
 * @version v18.0.1
 * @link http://www.ag-grid.com/
 * @license MIT
 */
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var utils_1 = require("../utils");
var TabbedLayout = (function () {
    function TabbedLayout(params) {
        var _this = this;
        this.items = [];
        this.params = params;
        this.eGui = document.createElement('div');
        this.eGui.innerHTML = TabbedLayout.TEMPLATE;
        this.eHeader = this.eGui.querySelector('[ref="tabHeader"]');
        this.eBody = this.eGui.querySelector('[ref="tabBody"]');
        utils_1.Utils.addCssClass(this.eGui, params.cssClass);
        if (params.items) {
            params.items.forEach(function (item) { return _this.addItem(item); });
        }
    }
    TabbedLayout.prototype.setAfterAttachedParams = function (params) {
        this.afterAttachedParams = params;
    };
    TabbedLayout.prototype.getMinWidth = function () {
        var eDummyContainer = document.createElement('span');
        // position fixed, so it isn't restricted to the boundaries of the parent
        eDummyContainer.style.position = 'fixed';
        // we put the dummy into the body container, so it will inherit all the
        // css styles that the real cells are inheriting
        this.eGui.appendChild(eDummyContainer);
        var minWidth = 0;
        this.items.forEach(function (itemWrapper) {
            utils_1.Utils.removeAllChildren(eDummyContainer);
            var eClone = itemWrapper.tabbedItem.bodyPromise.resolveNow(null, function (body) { return body.cloneNode(true); });
            if (eClone == null) {
                return;
            }
            eDummyContainer.appendChild(eClone);
            if (minWidth < eDummyContainer.offsetWidth) {
                minWidth = eDummyContainer.offsetWidth;
            }
        });
        this.eGui.removeChild(eDummyContainer);
        return minWidth;
    };
    TabbedLayout.prototype.showFirstItem = function () {
        if (this.items.length > 0) {
            this.showItemWrapper(this.items[0]);
        }
    };
    TabbedLayout.prototype.addItem = function (item) {
        var eHeaderButton = document.createElement('span');
        eHeaderButton.appendChild(item.title);
        utils_1.Utils.addCssClass(eHeaderButton, 'ag-tab');
        this.eHeader.appendChild(eHeaderButton);
        var wrapper = {
            tabbedItem: item,
            eHeaderButton: eHeaderButton
        };
        this.items.push(wrapper);
        eHeaderButton.addEventListener('click', this.showItemWrapper.bind(this, wrapper));
    };
    TabbedLayout.prototype.showItem = function (tabbedItem) {
        var itemWrapper = utils_1.Utils.find(this.items, function (itemWrapper) {
            return itemWrapper.tabbedItem === tabbedItem;
        });
        if (itemWrapper) {
            this.showItemWrapper(itemWrapper);
        }
    };
    TabbedLayout.prototype.showItemWrapper = function (wrapper) {
        var _this = this;
        if (this.params.onItemClicked) {
            this.params.onItemClicked({ item: wrapper.tabbedItem });
        }
        if (this.activeItem === wrapper) {
            utils_1.Utils.callIfPresent(this.params.onActiveItemClicked);
            return;
        }
        utils_1.Utils.removeAllChildren(this.eBody);
        wrapper.tabbedItem.bodyPromise.then(function (body) {
            _this.eBody.appendChild(body);
        });
        if (this.activeItem) {
            utils_1.Utils.removeCssClass(this.activeItem.eHeaderButton, 'ag-tab-selected');
        }
        utils_1.Utils.addCssClass(wrapper.eHeaderButton, 'ag-tab-selected');
        this.activeItem = wrapper;
        if (wrapper.tabbedItem.afterAttachedCallback) {
            wrapper.tabbedItem.afterAttachedCallback(this.afterAttachedParams);
        }
    };
    TabbedLayout.prototype.getGui = function () {
        return this.eGui;
    };
    TabbedLayout.TEMPLATE = '<div>' +
        '<div ref="tabHeader" class="ag-tab-header"></div>' +
        '<div ref="tabBody" class="ag-tab-body"></div>' +
        '</div>';
    return TabbedLayout;
}());
exports.TabbedLayout = TabbedLayout;
