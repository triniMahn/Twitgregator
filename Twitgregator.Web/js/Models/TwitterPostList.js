(function() {
  var _ref, _ref1,
    __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

  window.TwitterPostList = (function(_super) {
    __extends(TwitterPostList, _super);

    function TwitterPostList() {
      _ref = TwitterPostList.__super__.constructor.apply(this, arguments);
      return _ref;
    }

    TwitterPostList.prototype.initialize = function() {};

    return TwitterPostList;

  })(Backbone.Model);

  window.TwitterPostListCollection = (function(_super) {
    __extends(TwitterPostListCollection, _super);

    function TwitterPostListCollection() {
      _ref1 = TwitterPostListCollection.__super__.constructor.apply(this, arguments);
      return _ref1;
    }

    TwitterPostListCollection.prototype.model = TwitterPostList;

    TwitterPostListCollection.prototype.url = '/api/home';

    return TwitterPostListCollection;

  })(Backbone.Collection);

}).call(this);
