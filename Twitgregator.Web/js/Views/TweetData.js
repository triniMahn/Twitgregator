(function() {
  var _ref, _ref1,
    __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; },
    __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

  window.TweetDataView = (function(_super) {
    __extends(TweetDataView, _super);

    function TweetDataView() {
      _ref = TweetDataView.__super__.constructor.apply(this, arguments);
      return _ref;
    }

    TweetDataView.prototype.tagName = 'div';

    TweetDataView.prototype.template = _.template($('#tweetData-template').html());

    TweetDataView.prototype.initialize = function() {};

    TweetDataView.prototype.render = function() {
      var jsn;

      jsn = this.model.toJSON();
      this.$el.html(this.template(jsn));
      return this;
    };

    return TweetDataView;

  })(Backbone.View);

  window.TweetDataListView = (function(_super) {
    __extends(TweetDataListView, _super);

    function TweetDataListView() {
      this.display = __bind(this.display, this);
      this.render = __bind(this.render, this);      _ref1 = TweetDataListView.__super__.constructor.apply(this, arguments);
      return _ref1;
    }

    TweetDataListView.prototype.el = $('#actionWindowContainer');

    TweetDataListView.prototype.initialize = function() {
      return this.collection.on('all', this.render, this);
    };

    TweetDataListView.prototype.currentAnswerView = null;

    TweetDataListView.prototype.render = function() {
      this.collection.each(this.display);
      return this;
    };

    TweetDataListView.prototype.display = function(data) {
      var view;

      view = new TweetDataView({
        model: data
      });
      return this.$el.html(view.render().el);
    };

    return TweetDataListView;

  })(Backbone.View);

}).call(this);
