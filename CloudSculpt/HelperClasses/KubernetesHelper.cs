using System;
using k8s;

namespace KubernetesTest;

public static class KubernetesHelper
{
    // 1. Get Cluster Status- Done
    // 2. Create Deployments
    // 3. Remove Deployments

    public static bool GetClusterStatus()
    {
        try
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile("./Configs/kubeconfig");
            IKubernetes client = new Kubernetes(config);
            var nodes = client.CoreV1.ListNode();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}