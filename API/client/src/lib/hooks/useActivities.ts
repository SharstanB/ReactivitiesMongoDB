import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { Activity, ApiResponse } from "../../lib/types/index";
import { useNavigate } from "react-router";
// import axios from "axios";

export const useActivities = (id?: string) => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { data: activityResult, isPending } = useQuery({
    queryKey: ["activities"],
    queryFn: async () => {
      const response = await agent.get<ApiResponse<Activity[]>>("/Activities");
      return response.data.data;
    },
    staleTime: 1000 * 60 * 5,
  });
  const { data: activityDetail, isLoading: isLoadingActivity } = useQuery({
    queryKey: ["activities", id],
    queryFn: async () => {
      const response = await agent.get<ApiResponse<Activity>>(
        `Activities/${id}`
      );
      return response.data.data;
    },
    enabled: !!id,
    staleTime: 1000 * 60 * 5,
  });

  const createActivity = useMutation({
    mutationFn: async (activity: Activity) => {
      await agent.post("/Activities", activity).then((res) => {
        if (res?.data) navigate(`/activities/${res?.data.data}`);
      });
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["activities"],
      });
    },
  });
  const updateActivity = useMutation({
    mutationFn: async (activity: Activity) => {
      await agent.put("/Activities", activity);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["activities"],
      });
    },
  });
  const deleteActivity = useMutation({
    mutationFn: async (id: string) => {
      await agent.delete(`/Activities/${id}`);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["activities"],
      });
    },
  });

  return {
    activityResult,
    isPending,
    updateActivity,
    createActivity,
    deleteActivity,
    activityDetail,
    isLoadingActivity,
  };
};
