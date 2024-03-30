import { createStore } from 'vuex'; // Import createStore từ Vuex

import { account } from './modules/account';
import { alert } from './modules/alert';

const store = createStore({
  modules: {
    account, alert
  }
});

export default store;
