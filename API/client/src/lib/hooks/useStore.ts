import { useContext } from "react"
import { StoreContext } from "../../lib/stores/Store"
export const useStore = () => {
   return useContext(StoreContext);
}
