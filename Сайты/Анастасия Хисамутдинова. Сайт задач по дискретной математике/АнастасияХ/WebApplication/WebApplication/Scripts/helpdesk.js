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

// Таблицы
// Добавлено для "правильных" таблиц
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
            $detail.html('Loading from ajax request...');
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