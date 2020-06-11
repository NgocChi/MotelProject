﻿/*!
 * Bootstrap Confirmation 2.2.0
 * Copyright 2013 Nimit Suwannagate <ethaizone@hotmail.com>
 * Copyright 2014-2016 Damien "Mistic" Sorel <http://www.strangeplanet.fr>
 * Licensed under the Apache License, Version 2.0 (the "License")
 */
!function (a) {
    "use strict";
    function b(a) {
        for (var b = window, c = a.split("."), d = c.pop(), e = 0, f = c.length; f > e; e++)b = b[c[e]];
        return function () {
            b[d].call(this)
        }
    }

    if (!a.fn.popover) throw new Error("Confirmation requires popover.js");
    var c = function (b, c) {
        c.trigger = "click", this.init("confirmation", b, c), this.options._isDelegate = !1, c.selector ? this.options._selector = this._options._selector = c._root_selector + " " + c.selector : c._selector ? (this.options._selector = c._selector, this.options._isDelegate = !0) : this.options._selector = c._root_selector;
        var d = this;
        this.options.selector || (this.options._attributes = {}, this.options.copyAttributes ? "string" == typeof this.options.copyAttributes && (this.options.copyAttributes = this.options.copyAttributes.split(" ")) : this.options.copyAttributes = [], this.options.copyAttributes.forEach(function (a) {
            this.options._attributes[a] = this.$element.attr(a)
        }, this), this.$element.on(d.options.trigger, function (a, b) {
            b || (a.preventDefault(), a.stopPropagation(), a.stopImmediatePropagation())
        }), this.$element.on("show.bs.confirmation", function (b) {
            d.options.singleton && a(d.options._selector).not(a(this)).filter(function () {
                return void 0 !== a(this).data("bs.confirmation")
            }).confirmation("hide")
        })), this.options._isDelegate || (this.eventBody = !1, this.uid = this.$element[0].id || this.getUID("group_"), this.$element.on("shown.bs.confirmation", function (b) {
            if (d.options.popout && !d.eventBody) {
                a(this);
                d.eventBody = a("body").on("click.bs.confirmation." + d.uid, function (b) {
                    a(d.options._selector).is(b.target) || (a(d.options._selector).filter(function () {
                        return void 0 !== a(this).data("bs.confirmation")
                    }).confirmation("hide"), a("body").off("click.bs." + d.uid), d.eventBody = !1)
                })
            }
        }))
    };
    c.DEFAULTS = a.extend({}, a.fn.popover.Constructor.DEFAULTS, {
        placement: "top",
        title: "Bạn chắc chắn muốn xóa dữ liệu?",
        html: !0,
        popout: !1,
        singleton: !1,
        copyAttributes: "href target",
        onConfirm: a.noop,
        onCancel: a.noop,
        btnOkClass: "btn-xs btn-primary",
        btnOkIcon: "glyphicon glyphicon-ok",
        btnOkLabel: "Có",
        btnCancelClass: "btn-xs btn-default",
        btnCancelIcon: "glyphicon glyphicon-remove",
        btnCancelLabel: "Không",
        template: '<div class="popover confirmation"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content text-center"><div class="btn-group"><a class="btn" data-apply="confirmation" data-remote="true" data-method="delete"></a><a class="btn" data-dismiss="confirmation"></a></div></div></div>'
    }), c.prototype = a.extend({}, a.fn.popover.Constructor.prototype), c.prototype.constructor = c, c.prototype.getDefaults = function () {
        return c.DEFAULTS
    }, c.prototype.setContent = function () {
        var b = this, c = this.tip(), d = this.options;
        c.find(".popover-title")[d.html ? "html" : "text"](this.getTitle()), c.find('[data-apply="confirmation"]').addClass(d.btnOkClass).html(d.btnOkLabel).attr(this.options._attributes).prepend(a("<i></i>").addClass(d.btnOkIcon), " ").off("click").one("click", function (a) {
            b.getOnConfirm.call(b).call(b.$element), b.$element.trigger("confirmed.bs.confirmation"), b.$element.trigger(b.options.trigger, [!0]), b.$element.confirmation("hide")
        }), c.find('[data-dismiss="confirmation"]').addClass(d.btnCancelClass).html(d.btnCancelLabel).prepend(a("<i></i>").addClass(d.btnCancelIcon), " ").off("click").one("click", function (a) {
            b.getOnCancel.call(b).call(b.$element), b.inState && (b.inState.click = !1), b.$element.trigger("canceled.bs.confirmation"), b.$element.confirmation("hide")
        }), c.removeClass("fade top bottom left right in"), c.find(".popover-title").html() || c.find(".popover-title").hide()
    }, c.prototype.getOnConfirm = function () {
        return this.$element.attr("data-on-confirm") ? b(this.$element.attr("data-on-confirm")) : this.options.onConfirm
    }, c.prototype.getOnCancel = function () {
        return this.$element.attr("data-on-cancel") ? b(this.$element.attr("data-on-cancel")) : this.options.onCancel
    };
    var d = a.fn.confirmation;
    a.fn.confirmation = function (b) {
        var d = "object" == typeof b && b || {};
        return d._root_selector = this.selector, this.each(function () {
            var e = a(this), f = e.data("bs.confirmation");
            (f || "destroy" != b) && (f || e.data("bs.confirmation", f = new c(this, d)), "string" == typeof b && f[b]())
        })
    }, a.fn.confirmation.Constructor = c, a.fn.confirmation.noConflict = function () {
        return a.fn.confirmation = d, this
    }
}(jQuery);