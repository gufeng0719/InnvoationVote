var vm = new Vue({
    el: '#app',
    data: {
        list: [],
        imgBaseUrl: '/Images/',
        type1: [], //投票1类的
        type2: [], //投票2类
        type3: [], //投票3类
        openid: '', //公开openid,
        havote: {
            type1: [], //第一类别
            type2: [], //第二类别
            type3:[] //第三类别
        } //该天已经投票的ID
    },
    methods: {
        initData:function() {
            //加载本人今天已经投票ID
            var that = this;
            if (that.openid != '') {

            } else {
                that.openid = loadOpen();
            }
            ajax('/Vote/GetVote',
                { openid: that.openid },
                function (data) {
                    if (data.data.length > 0) {
                        var vote = JSON.parse(data.data[0].Prodjects); //得到的列表
                        that.havote.type1 = vote.type1;
                        that.havote.type2 = vote.type2;
                        that.havote.type3 = vote.type3;
                    }
                    //console.log(that.havote);
                });
        },
        getpage: function() {
            var that = this;
            ajax('/Vote/GetProjectList',
                {},
                function(data) {
                    that.type1 = data.type1;
                    that.type2 = data.type2;
                    that.type3 = data.type3;

                });
        },
        vote:function(project) {
            var that = this;
            if (that.openid == '') {//如果openid还未获取，则获取openid
                that.openid = loadOpen();
            } else {
                //开始投票
                if (project.TypeId == "1") {
                    that.havote.type1.push(parseInt(project.ProjectId));
                }else if (project.TypeId == "2") {
                    that.havote.type2.push(parseInt(project.ProjectId));
                } else {
                    that.havote.type3.push(parseInt(project.ProjectId));
                }
                project.VoteNumber = parseInt(project.VoteNumber) + 1;
                ajax('/Vote/SaveRecord',
                    {
                        openid: that.openid,
                        havote: JSON.stringify(that.havote),
                        projectid:project.ProjectId
                    },
                    function(data) {
                        if (data.code === 1) {
                            alert(data.data);
                        } else {
                            alert(data.data);
                        }
                    });
            }
        }
        
    },
    mounted: function () {
        this.getpage();
        this.initData();
    }
})