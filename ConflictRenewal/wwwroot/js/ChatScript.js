(function ($) {
    setInterval(function () {
            $.ajax({
                url: '/Chat/?handler=Notification',
                //data: { From: $('#SelectedUserId').val() },
                contentType: 'application/json; charset=utf-8',
                method: "Get",
                datatype: "json",
                cache: false,

                success: function (data) {
                    if (data != null || data != "") {
                        $('#NotificationLiveDiv').html(data);
                    }
                },
                failure: function (response) {
                    console.log(data);

                }
            });

    }, 2000);

    $(document).ready(function () {
        var $chatbox = $('.chatbox'),
            $chatboxTitle = $('.chatbox__title'),
            $chatboxTitleClose = $('.chatbox__title__close'),
            $chatboxCredentials = $('.chatbox__credentials');
        var ToId = $('#SelectedUserId').val();
        var CurrentUserId = $('#CurrentUserId').val();

        $chatLoaderDiv = $('#discussionDiv');
        $chatboxTitle.on('click', function () {
            $.ajax({
                type: "GET",
                url: "/Chat/?handler=SendMessage",
                contentType: "application/json",
                data: { id: ToId },
                dataType: "json",
                success: function (AuditTrail) {
                    $chatbox.toggleClass('chatbox--tray');
                    $.each(AuditTrail, function (i, item) {
                        var da = AuditTrail[i].date;
                        var d = new Date(parseInt(da));
                        if (AuditTrail[i].from == CurrentUserId) {
                            $chatLoaderDiv.append('<div class="chatbox__body__message chatbox__body__message--left"><div class="chatbox_timing"><ul><li><a href="#"><i class="fa fa-calendar"></i>' + d + '</a></li></ul></div><img src="/Uploads/ProfileImage/' + AuditTrail[i].image+'" alt="Picture"><div class="clearfix"></div><div class="ul_section_full"><ul class="ul_msg"><li><strong>' + AuditTrail[i].fromName +'</strong></li><li>' + AuditTrail[i].text + ' </li></ul><div class="clearfix"></div></div></div>');
                        }
                        else {
                            $chatLoaderDiv.append('<div class="chatbox__body__message chatbox__body__message--right"><div class="chatbox_timing"><ul><li><a href="#"><i class="fa fa-calendar"></i>' + d + '</a></li></ul></div><img src="/Uploads/ProfileImage/' + AuditTrail[i].image +'" alt="Picture"><div class="clearfix"></div><div class="ul_section_full"><ul class="ul_msg"><li><strong>' + AuditTrail[i].fromName +'</strong></li><li>' + AuditTrail[i].text + ' </li></ul><div class="clearfix"></div></div></div>');
                        }
                        });
                    
                    var container = document.getElementById("discussionDiv");
                    container.scrollTop = container.scrollHeight;
                },
                failure: function (response) {
                    alert(response);
                }
            });
        });
        $chatboxTitleClose.on('click', function (e) {
            e.stopPropagation();
            $chatbox.addClass('chatbox--closed');
        });
        $chatbox.on('transitionend', function () {
            if ($chatbox.hasClass('chatbox--closed')) $chatbox.remove();
        });
        
        $('#btnchat').click(function () {
            var Message = $.trim($("#btn-input").val());
            $('#btn-input').val('').focus();
            var ToId = $('#SelectedUserId').val();
            var ToName = $('#SelectedUserName').val();
            var FileName = $('input[name=ChatFileName]').val();
            var userPic = $('#profilepici').val();


            var CurrentUserName = $('#CurrentUserName').val();
            //var fileUpload = null;
            //if (FileName == "Photo") {
            //    fileUpload = $("input[name=ImageFile]").get(0);
            //} else if (FileName == "Video") {
            //    fileUpload = $("input[name=VideoFile]").get(0);
            //} else if (FileName == "File") {
            //    fileUpload = $("input[name=DocumentFile]").get(0);
            //}
            //var files = null;
            //if (fileUpload) {
            //    files = fileUpload.files;
            //}

            //var FilePath = null;
            var fileData = new FormData();


            //if (files) {
            //    if (files.length > 0) {
            //        FilePath = files[0].name;
            //    }
            //    for (var i = 0; i < files.length; i++) {
            //        fileData.append(files[i].name, files[i]);
            //    }
            //}

            //if (!files && Message.length < 1) {
            if (Message.length < 1) {
                $('#NotSendError').css("display", "");
                $('#NotSendError').html("Type some text to send.");
                $('#NotSendError').delay(3000).fadeOut();

                return;
            }

            fileData.append('btn-input', Message);
            fileData.append('To', ToId);
            //fileData.append('FileType', FileName);

            $('#btn-input').value = "";
            $('#btn-input').blur();
            $('#ChatForm').trigger("reset");

            $('#ImgpreviewLi').css("display", "none");
            $('#VideopreviewLi').css("display", "none");
            $('#FilepreviewLi').css("display", "none");

            if (ToId.length < 1 || ToName.length < 1) {
                $('#NotSendError').css("display", "");
                $('#NotSendError').html("Please select user to start chat.");
                $('#NotSendError').delay(3000).fadeOut();
                return;
            }

            else {
                $('#btn-input').removeClass('inputEror');
                $.ajax({
                    url: '/Chat/?handler=SendMessage',
                    type: "Post",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },

                   // data: { text: Message, To: ToId },
                    data: JSON.stringify({
                        text: Message,
                        To: ToId
                    }),
                    contentType: "application/json",
                    dataType: "json",

                    success: function (AuditTrail, xhr, response, results) {
                        if (response.status == "200") {
                            var d = new Date();
                            d + '';                  // "Sun Dec 08 2013 18:55:38 GMT+0100"
                            d.toLocaleString();

                            //if (FilePath == undefined && Message.length > 0) {
                            //    //alert("undefinded");
                            //    //$('#discussionDiv').append('<div class="row"> <div class="col-sm-6  "> <div class="wordWrap triangle-border-left left" >' + Message + ' </div> <div style="padding-left:19px"> <p  style="font-size: x-small;">' + d.toLocaleString() + '</p> </div></div> </div>');
                            //    $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + userPic + '" class="img-circle"/></span><div class="chat-body1 clearfix"><p>' + Message + ' </p><div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');

                            //}
                            //else if (FilePath != undefined && Message.length > 0) {
                            //    if (FileName == "Photo") {
                            //        //$('#discussionDiv').append('<div class="row"> <div class="col-sm-6  "> <div class="wordWrap triangle-border-left left" >' + Message + ' </div> <div style="padding-left:19px"><img src="/SendFiles/' + FilePath + '" class="img-thumbnail" style="padding:0px;" /> </div> <div style="padding-left:19px"> <p  style="font-size: x-small;">' + d.toLocaleString() + '</p> </div></div> </div>');
                            //        $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + userPic + '" class="img-circle"/></span><div class="chat-body1 clearfix">' + Message + ' </div> <div style="padding-left:19px"><img src="/Uploads/ChatImages/' + FilePath + '" class="img-thumbnail" style="padding:0px;width:300px;" /> </div> <div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');
                            //    }
                            //}
                            //else if (FilePath != undefined && Message.length < 1) {
                            //    if (FileName == "Photo") {
                            //        //$('#discussionDiv').append('<div class="row"> <div class="col-sm-6  "> <div style="padding-left:19px"><img src="/SendFiles/' + FilePath + '" class="img-thumbnail" style="padding:0px;" /> </div> <div style="padding-left:19px"> <p  style="font-size: x-small;">' + d.toLocaleString() + '</p> </div></div> </div>');
                            //        $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + userPic + '" class="img-circle"/></span><div style="padding-left:19px"><img src="/Uploads/ChatImages/' + FilePath + '" class="img-thumbnail" style="padding:0px;width:300px;" /> </div> <div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');
                            //    }
                            //}

                            // $(".chat_area").animate({ scrollTop: $('ul#discussionDiv li:last').offset().top + 30 });

                            //  $.each(AuditTrail, function (i, item) {
                            // $('.chat_area').scrollToBottom();
                            $chatLoaderDiv.append('<div class="chatbox__body__message chatbox__body__message--left"><div class="chatbox_timing"><ul><li><a href="#"><i class="fa fa-calendar"></i>' + AuditTrail.date + '</a></li></ul></div><img src="https://www.gstatic.com/webp/gallery/2.jpg" alt="Picture"><div class="clearfix"></div><div class="ul_section_full"><ul class="ul_msg"><li><strong>Person Name</strong></li><li>' + AuditTrail.text + ' </li></ul><div class="clearfix"></div><ul class="ul_msg2"><li></li></ul></div></div>');
                            $(".chat_area").animate({ scrollTop: $('.chat_area').scrollTop() + $('.chat_area').height() });
                            //get container element and scroll down
                            var container = document.getElementById("discussionDiv");
                            container.scrollTop = container.scrollHeight;

                        }
                    },
                    failure: function (response) {
                        alert(response);
                        $('#NotSendError').css("display", "");
                        $('#NotSendError').text("Message sending fail !");
                        $('#NotSendError').delay(4000).fadeOut();
                    },

                });

            }

        });

    });
})(jQuery);
