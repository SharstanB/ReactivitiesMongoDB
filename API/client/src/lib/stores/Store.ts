import AuthStore from "./AuthStore";
import { createContext } from "react";
import { UiStore } from "./UIStore";
interface Store {
  authStore: AuthStore;
  uiStore: UiStore;
}

export const store: Store = {
  authStore: new AuthStore(),
  uiStore: new UiStore(),
}
export const StoreContext = createContext<Store>(store);
