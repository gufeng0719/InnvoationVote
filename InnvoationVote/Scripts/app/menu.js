﻿var mentApp = new Vue({
    el: '#menuApp',
    data: {
        menuList: [{ "id": "0-1", "title": "投票查询", "icon": "el-icon-document", "url": "/Home", "child": [] }],
        defaultActive: "0-1"
    },
    methods: {
        routeTo: function(item) {
            window.location.href = item.url;
        }
    },
    mounted: function() {
        var that = this;
        var thatUrl = document.URL;
        var menuElement = document.getElementsByClassName('el-menu-vertical');
        menuElement[0].style.height = document.body.scrollHeight - 90 + 'px';
        setInterval(function() {
                if (document.body.scrollHeight - 90 + "px" != menuElement[0].style.height)
                    menuElement[0].style.height = document.body.scrollHeight - 90 + 'px';
            },
            300);

    }
});

function signout(userName) {
    ajax('/Login/LoginOut',
        {
            userName: userName
        },
        function(data) {
            if (data.code === 1) {
                window.location.href = '/Login';
            }
        });
}