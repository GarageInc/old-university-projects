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