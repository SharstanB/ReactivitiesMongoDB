import { useQuery } from "@tanstack/react-query";
import agent from "../api/agent";
import {BasicListObject, ApiResponse} from '../../lib/types/index'

export const useCities = () => {
    const {data: citieslist} =  useQuery({
        queryKey: ['cities'],
          queryFn: async () => {
            const response = await agent.get<ApiResponse<BasicListObject[]>>('/Cities');
            console.log(response.data.data);
          return response.data.data;
        },
        staleTime: 1000 * 60 * 5
      });

  return {
    citieslist
  };
}
