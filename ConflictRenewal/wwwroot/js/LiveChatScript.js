
$(function () {
    setInterval(function () {
        // $('#NotificationDiv').load('/Chat/OnGet_Notification');
        $.ajax({
            url: '/Chat/?handler=Notification',
            //data: { From: $('#SelectedUserId').val() },
            contentType: 'application/json; charset=utf-8',
            method: "Get",
            datatype: "json",
            cache: false,

            // }).done(function (data, xhr, response) {
            success: function (data) {
                debugger;
                if (data != null || data != "") {
                    $('#NotificationDiv').html(data);
                }
            },
            failure: function (response) {
                console.log(data);

            }
        });

    //}, 2000);

    ////setInterval(function () { $('#UserDiv').load('/Chat/_AddedUsers'); }, 2000);

    //setInterval(function () {
        var SelectedUserId = $.trim($('#SelectedUserId').val());
        if (SelectedUserId.length > 0) {

            $.ajax({
                url: '/Chat/?handler=GetUnreadConversation', 
                data: { From: $('#SelectedUserId').val() },
                contentType: 'application/json; charset=utf-8',
                method: "Get",
                datatype: "json",
                cache: false,

                // }).done(function (data, xhr, response) {
                success: function (data) {
                        if (data.length > 0) {

                            for (var i = 0; i < data.length; i++) {
                                var da = data[i].date;
                              //  da = da.replace(/[^0-9 +]/g, '');
                                var d = new Date(parseInt(da));

                                d + '';                  // "Sun Dec 08 2013 18:55:38 GMT+0100"
                                d.toLocaleString();
                                if (data[i].from == CurrentUserId) {
                                    var ImageName = data[i].image;
                                    var VideoName = data[i].video;
                                    var FileName = data[i].file;
                                    if (data[i].text != "" && (ImageName == null && VideoName == null && FileName == null)) {
                                        $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + data[i].fromImage + '" class="img-circle"/></span><div class="chat-body1 clearfix"><p>' + data[i].text + ' </p><div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');
                                    }
                                    else if (data[i].text != "" && (ImageName != null || VideoName != null || FileName != null)) {
                                        if (VideoName == null && FileName == null && ImageName != null) {
                                            $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + data[i].fromImage + '" class="img-circle"/></span><div class="chat-body1 clearfix">' + data[i].text + ' </div> <div style="padding-left:19px"><img src="/Uploads/ChatImages/' + ImageName + '" class="img-thumbnail" style="padding:0px;width:300px;" /> </div> <div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');
                                        }
                                    }
                                    else if (data[i].text == "" && (ImageName != null || VideoName != null || FileName != null)) {
                                        if (VideoName == null && FileName == null && ImageName != null) {
                                            $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + data[i].fromImage + '" class="img-circle"/></span><div style="padding-left:19px"><img src="/Uploads/ChatImages/' + ImageName + '" class="img-thumbnail" style="padding:0px;width:300px;" /> </div> <div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');
                                        }
                                    }

                                }
                                else {
                                    var ImageName = data[i].image;
                                    var VideoName = data[i].video;
                                    var FileName = data[i].file;

                                    if (data[i].Text != "" && (ImageName == null && VideoName == null && FileName == null)) {
                                        $('#discussionDiv').append('<li class="left clearfix admin_chat"> <li class="left clearfix"><span class="chat-img1 pull-right"><img src="/Uploads/ProfileImage/' + data[i].fromImage + '" class="img-circle"/></span><div class="chat-body1 clearfix"><p>' + data[i].text + ' </p><div class="chat_time pull-left">' + d.toLocaleString() + '</div></div> </li>');
                                    }
                                    else if (data[i].Text != "" && (ImageName != null || VideoName != null || FileName != null)) {
                                        if (VideoName == null && FileName == null && ImageName != null) {
                                            $('#discussionDiv').append('<li class="left clearfix admin_chat"> <li class="left clearfix"><span class="chat-img1 pull-right"><img src="/Uploads/ProfileImage/' + data[i].fromImage + '" class="img-circle"/></span><div class="chat-body1 clearfix">' + data[i].text + ' </div> <div style="padding-left:19px;width:300;height:220;"><img src="/Uploads/ChatImages/' + ImageName + '" class="img-thumbnail" style="padding:0px;" /> </div> <div class="chat_time pull-left">' + d.toLocaleString() + '</div></div> </li>');
                                        }
                                    }
                                    else if (data[i].text == "" && (ImageName != null || VideoName != null || FileName != null)) {
                                        if (VideoName == null && FileName == null && ImageName != null) {
                                            $('#discussionDiv').append('<li class="left clearfix admin_chat"> <li class="left clearfix"><span class="chat-img1 pull-right"><img src="/Uploads/ProfileImage/' + data[i].fromImage + '" class="img-circle"/></span><div style="padding-left:19px"><img src="/Uploads/ChatImages/' + ImageName + '" class="img-thumbnail" style="padding:0px;width:300;height:220;" /> </div> <div class="chat_time pull-left">' + d.toLocaleString() + '</div></div> </li>');
                                        }
                                    }


                                }
                            }
                            $(".chat_area").animate({ scrollTop: $('.chat_area').scrollTop() + $('.chat_area').height() });
                        }
                    
                },
                failure: function (response) {
                    console.log(data);

                }
            });
        }
    }, 2000);
});

function GetLatestChat() {
    
    $.ajax({
        url: '/Chat/?handler=GetLastestChat',
        contentType: 'application/json; charset=utf-8',
        method: "Get",
        datatype: "json",
        cache: false

    }).done(function (data, xhr, response) {
        if (response.status == "200") {
            if (data.length > 0) {
                $('#LatestChatDiv').html("");
                $.each(data, function (index, value) {
                    $('#LatestChatDiv').append('<a href="/Chat/Index/' + data[index].From + '" style="color:black"><div class="row" style="margin-top:5px;"> <div class="col-sm-2">' +
                        '<img src="/Uploads/ProfileImage/' + data[index].Image + '" style="width:30px; class="img-thumbnail"" /></div>' +
                        '<div class="col-sm-10" style="padding-left: 0px;">' +
                        '<strong>' + data[index].FromName + '</strong> <br />' +
                        '<small>' + data[index].Text + '</small> </div> <hr /></a>');
                });
            } else {
                $('#LatestChatDiv').html("");
                $('#LatestChatDiv').append('<strong> No recent chat found</strong> <br />');
                //$('#chatBoxArea').css("display", "block");
            }
        }
    }).error(function (data, xhr, response) {
        console.log(data)

    });
}

$(document).ready(function () {

    //$('#UserList').perfectScrollbar();
    //$('#discussionDiv').perfectScrollbar();

    $("#message").keypress(function (e) {

        if (e.which == 13) {

            $('#sendmessage').click();
        }
    });

    $("#SearchName").keypress(function (e) {

        if (e.which == 13) {

            $('#searchForm').submit();
        }
    });

    $('#sendmessage').click(function () {
        var Message = $.trim($("#message").val());
        $('#message').val('').focus();
        var ToId = $('#SelectedUserId').val();
        var ToName = $('#SelectedUserName').val();
        var FileName = $('input[name=ChatFileName]').val();
        var userPic = $('#profilepici').val();


        var CurrentUserName = $('#CurrentUserName').val();
        var fileUpload = null;
        if (FileName == "Photo") {
            fileUpload = $("input[name=ImageFile]").get(0);
        } else if (FileName == "Video") {
            fileUpload = $("input[name=VideoFile]").get(0);
        } else if (FileName == "File") {
            fileUpload = $("input[name=DocumentFile]").get(0);
        }
        var files = null;
        if (fileUpload) {
            files = fileUpload.files;
        }

        var FilePath = null;
        var fileData = new FormData();


        if (files) {
            if (files.length > 0) {
                FilePath = files[0].name;
            }
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
        }

        if (!files && Message.length < 1) {
            $('#NotSendError').css("display", "");
            $('#NotSendError').html("Type some text to send.")
            $('#NotSendError').delay(3000).fadeOut();

            return;
        }

        fileData.append('Message', Message);
        fileData.append('To', ToId);
        fileData.append('FileType', FileName);

        $('#message').value = "";
        $('#message').blur();
        $('#ChatForm').trigger("reset");

        $('#ImgpreviewLi').css("display", "none");
        $('#VideopreviewLi').css("display", "none");
        $('#FilepreviewLi').css("display", "none");

        if (ToId.length < 1 || ToName.length < 1) {
            $('#NotSendError').css("display", "");
            $('#NotSendError').html("Please select user to start chat.")
            $('#NotSendError').delay(3000).fadeOut();
            return;
        }

        else {
            $('#message').removeClass('inputEror');
            $.ajax({
                url: '/Chat/SendMessage',
                method: "Post",
                contentType: false,
                processData: false,
                data: fileData,

            }).done(function (data, xhr, response, results) {
                if (response.status == "200") {
                    var d = new Date();
                    d + '';                  // "Sun Dec 08 2013 18:55:38 GMT+0100"
                    d.toLocaleString();

                    if (FilePath == undefined && Message.length > 0) {
                        //alert("undefinded");
                        //$('#discussionDiv').append('<div class="row"> <div class="col-sm-6  "> <div class="wordWrap triangle-border-left left" >' + Message + ' </div> <div style="padding-left:19px"> <p  style="font-size: x-small;">' + d.toLocaleString() + '</p> </div></div> </div>');
                        $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + userPic + '" class="img-circle"/></span><div class="chat-body1 clearfix"><p>' + Message + ' </p><div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');

                    }
                    else if (FilePath != undefined && Message.length > 0) {
                        if (FileName == "Photo") {
                            //$('#discussionDiv').append('<div class="row"> <div class="col-sm-6  "> <div class="wordWrap triangle-border-left left" >' + Message + ' </div> <div style="padding-left:19px"><img src="/SendFiles/' + FilePath + '" class="img-thumbnail" style="padding:0px;" /> </div> <div style="padding-left:19px"> <p  style="font-size: x-small;">' + d.toLocaleString() + '</p> </div></div> </div>');
                            $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + userPic + '" class="img-circle"/></span><div class="chat-body1 clearfix">' + Message + ' </div> <div style="padding-left:19px"><img src="/Uploads/ChatImages/' + FilePath + '" class="img-thumbnail" style="padding:0px;width:300px;" /> </div> <div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');
                        }
                    }
                    else if (FilePath != undefined && Message.length < 1) {
                        if (FileName == "Photo") {
                            //$('#discussionDiv').append('<div class="row"> <div class="col-sm-6  "> <div style="padding-left:19px"><img src="/SendFiles/' + FilePath + '" class="img-thumbnail" style="padding:0px;" /> </div> <div style="padding-left:19px"> <p  style="font-size: x-small;">' + d.toLocaleString() + '</p> </div></div> </div>');
                            $('#discussionDiv').append('<li class="left clearfix"> <li class="left clearfix"><span class="chat-img1 pull-left"><img src="/Uploads/ProfileImage/' + userPic + '" class="img-circle"/></span><div style="padding-left:19px"><img src="/Uploads/ChatImages/' + FilePath + '" class="img-thumbnail" style="padding:0px;width:300px;" /> </div> <div class="chat_time pull-right">' + d.toLocaleString() + '</div></div> </li>');
                        }
                    }

                    // $(".chat_area").animate({ scrollTop: $('ul#discussionDiv li:last').offset().top + 30 });
                    $(".chat_area").animate({ scrollTop: $('.chat_area').scrollTop() + $('.chat_area').height() });
                }
            }).error(function (data, xhr, response) {
                $('#NotSendError').css("display", "");
                $('#NotSendError').text("Message sending fail !");
                $('#NotSendError').delay(4000).fadeOut();

            });

        }

    });


    $('#VideoFile').on('change', function () {
        var file = this.files[0];
        var fileType = $('input[name=ChatFileName]').val("Video");

        var sizeLimit = 20;
        var size = this.files[0].size;
        var total = size / 1024 / 1024;
        if (total > sizeLimit) {
            $('#ChatForm').bootstrapValidator('resetForm', true);
            $('#NotSendError').css("display", "");
            $('#NotSendError').text("File size should not exceed 10 mb.");
            $('#NotSendError').delay(3000).fadeOut();
            $('input[name=ChatFileName]').val("");
            return;
        }

        $('#VideopreviewLi').css("display", "");
        $('#ImgpreviewLi').css("display", "none");
        $('#FilepreviewLi').css("display", "none");

    });
    $('#ImageFile').on('change', function () {
        var file = this.files[0];
        var fileType = $('input[name=ChatFileName]').val("Photo");

        var sizeLimit = 10;
        var size = this.files[0].size;
        var total = size / 1024 / 1024;
        if (total > sizeLimit) {
            $('#ChatForm').bootstrapValidator('resetForm', true);
            $('#NotSendError').css("display", "");
            $('#NotSendError').text("File size should not exceed 10 mb.");
            $('#NotSendError').delay(3000).fadeOut();
            $('input[name=ChatFileName]').val("");
            return;
        }

        $('#VideopreviewLi').css("display", "none");
        $('#ImgpreviewLi').css("display", "");
        $('#FilepreviewLi').css("display", "none");
        readURL(this);
    });
    $('#DocumentFile').on('change', function () {
        var file = this.files[0];
        var fileType = $('input[name=ChatFileName]').val("File");

        var sizeLimit = 20;
        var size = this.files[0].size;
        var total = size / 1024 / 1024;
        if (total > sizeLimit) {
            $('#ChatForm').bootstrapValidator('resetForm', true);
            $('#NotSendError').css("display", "");
            $('#NotSendError').text("File size should not exceed 10 mb.");
            $('#NotSendError').delay(3000).fadeOut();
            $('input[name=ChatFileName]').val("");
            return;
        }

        $('#VideopreviewLi').css("display", "none");
        $('#ImgpreviewLi').css("display", "none");
        $('#FilepreviewLi').css("display", "");

    });
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#ImgpreviewLi').css("display", "");
            $('#previewImg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function SendVideo() {
    $('input[name=VideoFile]').click();
}
function SendPhoto() {
    $('input[name=ImageFile]').click();
}
function SendFile() {
    $('input[name=DocumentFile]').click();
}




