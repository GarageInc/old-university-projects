$(document).ready(function () {
             
    $.ajaxSetup({ cache: false });
 
            $(".openDialog").live("click", function (e) {
                e.preventDefault();
 
                $("<div></div>")
                    .addClass("dialog")
                    .attr("id", $(this)
                        .attr("data-dialog-id"))
                    .appendTo("body")
                    .dialog({
                        title: $(this).attr("data-dialog-title"),
                        close: function () { $(this).remove() },
                        modal: true
                    })
                    .load(this.href);
            });
 
            $(".close").live("click", function (e) {
                e.preventDefault();
                $(this).closest(".dialog").dialog("close");
      });
});

// Модальное окно снизу справа
$(function () {
    $.ajaxSetup({ cache: false });
    $(".compItem").click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });
    });
});

// Виджет кнопки вверх (Test-Templates)
; (function ($) { var h = $.scrollTo = function (a, b, c) { $(window).scrollTo(a, b, c) }; h.defaults = { axis: 'xy', duration: parseFloat($.fn.jquery) >= 1.3 ? 0 : 1, limit: true }; h.window = function (a) { return $(window)._scrollable() }; $.fn._scrollable = function () { return this.map(function () { var a = this, isWin = !a.nodeName || $.inArray(a.nodeName.toLowerCase(), ['iframe', '#document', 'html', 'body']) != -1; if (!isWin) return a; var b = (a.contentWindow || a).document || a.ownerDocument || a; return /webkit/i.test(navigator.userAgent) || b.compatMode == 'BackCompat' ? b.body : b.documentElement }) }; $.fn.scrollTo = function (e, f, g) { if (typeof f == 'object') { g = f; f = 0 } if (typeof g == 'function') g = { onAfter: g }; if (e == 'max') e = 9e9; g = $.extend({}, h.defaults, g); f = f || g.duration; g.queue = g.queue && g.axis.length > 1; if (g.queue) f /= 2; g.offset = both(g.offset); g.over = both(g.over); return this._scrollable().each(function () { if (e == null) return; var d = this, $elem = $(d), targ = e, toff, attr = {}, win = $elem.is('html,body'); switch (typeof targ) { case 'number': case 'string': if (/^([+-]=)?\d+(\.\d+)?(px|%)?$/.test(targ)) { targ = both(targ); break } targ = $(targ, this); if (!targ.length) return; case 'object': if (targ.is || targ.style) toff = (targ = $(targ)).offset() } $.each(g.axis.split(''), function (i, a) { var b = a == 'x' ? 'Left' : 'Top', pos = b.toLowerCase(), key = 'scroll' + b, old = d[key], max = h.max(d, a); if (toff) { attr[key] = toff[pos] + (win ? 0 : old - $elem.offset()[pos]); if (g.margin) { attr[key] -= parseInt(targ.css('margin' + b)) || 0; attr[key] -= parseInt(targ.css('border' + b + 'Width')) || 0 } attr[key] += g.offset[pos] || 0; if (g.over[pos]) attr[key] += targ[a == 'x' ? 'width' : 'height']() * g.over[pos] } else { var c = targ[pos]; attr[key] = c.slice && c.slice(-1) == '%' ? parseFloat(c) / 100 * max : c } if (g.limit && /^\d+$/.test(attr[key])) attr[key] = attr[key] <= 0 ? 0 : Math.min(attr[key], max); if (!i && g.queue) { if (old != attr[key]) animate(g.onAfterFirst); delete attr[key] } }); animate(g.onAfter); function animate(a) { $elem.animate(attr, f, g.easing, a && function () { a.call(this, e, g) }) } }).end() }; h.max = function (a, b) { var c = b == 'x' ? 'Width' : 'Height', scroll = 'scroll' + c; if (!$(a).is('html,body')) return a[scroll] - $(a)[c.toLowerCase()](); var d = 'client' + c, html = a.ownerDocument.documentElement, body = a.ownerDocument.body; return Math.max(html[scroll], body[scroll]) - Math.min(html[d], body[d]) }; function both(a) { return typeof a == 'object' ? a : { top: a, left: a } } })(jQuery);

jQuery.extend(jQuery.fn, {
    toplinkwidth: function () {
        totalContentWidth = jQuery('#content').outerWidth(); // ширина блока с контентом, включая padding
        totalTopLinkWidth = jQuery('#top-link').children('a').outerWidth(true); // ширина самой кнопки наверх, включая padding и margin

        h = (jQuery(window).width() - totalContentWidth) / 2 - totalTopLinkWidth - 65;
        if (h < 0) {
            // если кнопка не умещается, скрываем её
            jQuery(this).hide();
        } else {
            if (jQuery(window).scrollTop() >= 1) {
                jQuery(this).show();
            }
            jQuery(this).css({ 'padding-right': h + 'px', 'padding-top': 50 + 'px', 'background': 'none', 'color': '#fff' });
        }
    }
});

jQuery(function ($) {
    var topLink = $('#top-link');
    topLink.css({ 'padding-bottom': $(window).height() });
    // если вам не нужно, чтобы кнопка подстраивалась под ширину экрана - удалите следующие четыре строчки в коде
    topLink.toplinkwidth();
    $(window).resize(function () {
        topLink.toplinkwidth();
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() >= 1) {
            topLink.fadeIn(300).children('a').html('▲ Наверх').parent().removeClass('bottom_button').addClass('top_button');
        } else {
            topLink.children('a').html('▼ Вниз').parent().removeClass('top_button').addClass('bottom_button');
        }
    });
    topLink.click(function (e) {
        if ($(this).hasClass('bottom_button')) {
            // при нажатии на кнопку «Вниз» переходим туда, где прекратили чтение
            $("body").scrollTo(pos + 'px', 500);
        } else {
            // определяем и запоминаем координаты того места страницы, откуда был совершен переход наверх
            pos = (window.pageYOffset !== undefined) ? window.pageYOffset : (document.documentElement || document.body.parentNode || document.body).scrollTop;
            $("body,html").animate({ scrollTop: 0 }, 500);
        }
        return false;
    });
});

jQuery('#top-link').hover(
    function () {
        jQuery(this).animate({
            'opacity': '1',
        }).css({ 'background-color': '#e7ebf0', 'color': '#6a86a4' });
    },
    function () {
        jQuery(this).animate({
            'opacity': '0.7'
        }).css({ 'background': 'none', 'color': '#d3dbe4' });;
    });

jQuery('#top').hover(
    function () {
        jQuery(this).animate({
            'opacity': '1',
        }).css({ 'background-color': '#e7ebf0', 'color': '#6a86a4' });
    },
    function () {
        jQuery(this).animate({
            'opacity': '0.7'
        }).css({ 'background': 'none', 'color': '#d3dbe4' });;
    });


//Карусель
!function ($) {
    "use strict"; var Carousel = function (element, options) {
        this.$element = $(element)
        this.options = options
        this.options.slide && this.slide(this.options.slide)
        this.options.pause == 'hover' && this.$element.on('mouseenter', $.proxy(this.pause, this)).on('mouseleave', $.proxy(this.cycle, this))
    }
    Carousel.prototype = {
        cycle: function (e) {
            if (!e) this.paused = false
            this.options.interval && !this.paused && (this.interval = setInterval($.proxy(this.next, this), this.options.interval))
            return this
        }, to: function (pos) {
            var $active = this.$element.find('.active'), children = $active.parent().children(), activePos = children.index($active), that = this
            if (pos > (children.length - 1) || pos < 0) return
            if (this.sliding) { return this.$element.one('slid', function () { that.to(pos) }) }
            if (activePos == pos) { return this.pause().cycle() }
            return this.slide(pos > activePos ? 'next' : 'prev', $(children[pos]))
        }, pause: function (e) {
            if (!e) this.paused = true
            clearInterval(this.interval)
            this.interval = null
            return this
        }, next: function () {
            if (this.sliding) return
            return this.slide('next')
        }, prev: function () {
            if (this.sliding) return
            return this.slide('prev')
        }, slide: function (type, next) {
            var $active = this.$element.find('.active'), $next = next || $active[type](), isCycling = this.interval, direction = type == 'next' ? 'left' : 'right', fallback = type == 'next' ? 'first' : 'last', that = this, e = $.Event('slide')
            this.sliding = true
            isCycling && this.pause()
            $next = $next.length ? $next : this.$element.find('.item')[fallback]()
            if ($next.hasClass('active')) return
            if ($.support.transition && this.$element.hasClass('slide')) {
                this.$element.trigger(e)
                if (e.isDefaultPrevented()) return
                $next.addClass(type)
                $next[0].offsetWidth
                $active.addClass(direction)
                $next.addClass(direction)
                this.$element.one($.support.transition.end, function () {
                    $next.removeClass([type, direction].join(' ')).addClass('active')
                    $active.removeClass(['active', direction].join(' '))
                    that.sliding = false
                    setTimeout(function () { that.$element.trigger('slid') }, 0)
                })
            } else {
                this.$element.trigger(e)
                if (e.isDefaultPrevented()) return
                $active.removeClass('active')
                $next.addClass('active')
                this.sliding = false
                this.$element.trigger('slid')
            }
            isCycling && this.cycle()
            return this
        }
    }
    $.fn.carousel = function (option) {
        return this.each(function () {
            var $this = $(this), data = $this.data('carousel'), options = $.extend({}, $.fn.carousel.defaults, typeof option == 'object' && option)
            if (!data) $this.data('carousel', (data = new Carousel(this, options)))
            if (typeof option == 'number') data.to(option)
            else if (typeof option == 'string' || (option = options.slide)) data[option]()
            else if (options.interval) data.cycle()
        })
    }
    $.fn.carousel.defaults = { interval: 5000, pause: 'hover' }
    $.fn.carousel.Constructor = Carousel
    $(function () {
        $('body').on('click.carousel.data-api', '[data-slide]', function (e) {
            var $this = $(this), href, $target = $($this.attr('data-target') || (href = $this.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '')), options = !$target.data('modal') && $.extend({}, $target.data(), $this.data())
            $target.carousel(options)
            e.preventDefault()
        })
    })
}(window.jQuery);

// Кнопка по отправке сообщения об ошибке
$(function () {
    $.ajaxSetup({ cache: false });
    $("#messageButton").click(function (e) {
        var n = noty({
            layout: 'centerLeft', // Положение уведомлялки (top, topLeft, topCenter, topRight, centerLeft, center, centerRight, bottomLeft, bottomCenter, bottomRight, bottom) 
            theme: 'relax', // Тема по-умолчанию 
            type: 'success', // Тип окна (alert, success, error, warning, information, confirm) 
            text: 'Спасибо за сообщение об ошибке! <br/> С Вами наш сайт станет лучше!', // Текст. Можно использовать html теги 
            dismissQueue: true, // Не останавливать обработчик 
            template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>', // Шаблон по-умолчанию 
            animation: { // Анимация 
                open: { height: 'toggle' }, // Анимация высоты при открытии 
                close: { height: 'toggle' }, // Анимация выосты при закрытии 
                easing: 'swing', // Тип анимации 
                speed: 500 // Скорость анимации 
            },
            timeout: 5000, // Через сколько закрывать окно. Значение 3000 закроет окно через 3 сек 
            force: false, // Добавлять ли новые уведомления НАД старыми 
            modal: false, // Модальное ли окно 
            maxVisible: 10, // Максимальное количество окон 
            killer: false, // Закрывать ли все старые уведомления при вызове нового 
            closeWith: ['click'], // При каком событии закрывать окна ['click', 'button', 'hover'] 
            callback: { // События 
                onShow: function () { }, // До того, как окно появится 
                afterShow: function () { }, // После того, как окно появится 
                onClose: function () { }, // До закрытия окна 
                afterClose: function () { } // После закрытия окна 
            },
            buttons: false // Массив из кнопок
        });

    });
});

// Таблица

// Добавлено/ Нихуя не работает 
var $table = $('#table'),
$remove = $('#remove'),
selections = [];

$(function () {
    $table.bootstrapTable({
        columns: [
            [
                {
                    field: 'state',
                    checkbox: true,
                    rowspan: 1,
                    align: 'center',
                    valign: 'middle'
                }
            ],
            [
            ]
        ]
    });
    // sometimes footer render error.
    setTimeout(function () {
        $table.bootstrapTable('resetView');
    }, 200);
    $table.on('check.bs.table uncheck.bs.table ' +
            'check-all.bs.table uncheck-all.bs.table', function () {
                $remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
                // save your data, here just save the current page
                selections = getIdSelections();
                // push or splice the selections if you want to save all data selections
            });
    $table.on('expand-row.bs.table', function (e, index, row, $detail) {
        if (index % 2 == 1) {
            $detail.html('Loading from ajax Request...');
            $.get('LICENSE', function (res) {
                $detail.html(res.replace(/\n/g, '<br>'));
            });
        }
    });
    $table.on('all.bs.table', function (e, name, args) {
        console.log(name, args);
    });
    $remove.click(function () {
        var ids = getIdSelections();
        $table.bootstrapTable('remove', {
            field: 'id',
            values: ids
        });
        $remove.prop('disabled', true);
    });
    $(window).resize(function () {
        $table.bootstrapTable('resetView', {
            //height: getHeight()
        });
    });
});

function getIdSelections() {
    return $.map($table.bootstrapTable('getSelections'), function (row) {
        return row.id
    });
}
function responseHandler(res) {
    $.each(res.rows, function (i, row) {
        row.state = $.inArray(row.id, selections) !== -1;
    });
    return res;
}

function getHeight() {
    return $(window).height() - $('h1').outerHeight(true);
}