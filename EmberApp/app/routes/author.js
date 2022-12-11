import Route from '@ember/routing/route';
import { inject as service } from '@ember/service';
import { Promise } from 'rsvp';
import { later } from '@ember/runloop';

export default Route.extend({
  dataService: service('data'),
  queryParams: {
    search: {
      refreshModel: true
    }
  },
  model({ search }) {
    let promise = new Promise((resolve, reject) => {
      later(async () => {
        try {
          let authors = await this.get("dataService").getAuthors();
          console.log(authors);
          resolve(authors);
        }
        catch (e) {
          console.log(e);
          reject('Connection failed');
        }
      }, 1000);
    }).
    then((authors) => {
      this.set('controller.model', authors);
    }).
    finally(() => {
      if (promise === this.get('modelPromise')) {
        this.set('controller.isLoading', false);
      }
    });

    this.set('modelPromise', promise);
    return { isLoading: true };
  },

  setupController(controller, model) {
    this._super(...arguments);
    if (this.get('modelPromise')) {
      controller.set('isLoading', true);
    }
  },

  resetController(controller, isExiting, transition) {
    this._super(...arguments);
    if (isExiting) {
      controller.set('isLoading', false);
      this.set('modelPromise', null);
    }
  },

  actions: {
    refreshAuthors() {
      this.refresh();
    },
    loading(transition, originRoute) {
      return false;
    }
  }
});
