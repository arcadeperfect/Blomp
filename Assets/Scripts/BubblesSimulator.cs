using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace DefaultNamespace
{
    public class BubblesSimulator: MonoBehaviour
    {
        public static List<Rigidbody2D> Simulatables = new List<Rigidbody2D>();
        
        public float gravitationalConstantt = 10;
        public float minDistance = 0.1f;
        
        void Update()
        {
            // print(Simulatables.Count);
        }

        void FixedUpdate()
        {
            GravitySimulate();
        }

        
        private void GravitySimulate()
        {
            gravitationalConstantt = FindObjectOfType<Manager>().gravConstant;
            
            int objectCount = Simulatables.Count;
            if (objectCount == 0) return;

            // Allocate NativeArrays for job data
            NativeArray<Vector2> positions = new NativeArray<Vector2>(objectCount, Allocator.TempJob);
            NativeArray<float> masses = new NativeArray<float>(objectCount, Allocator.TempJob);
            NativeArray<Vector2> forces = new NativeArray<Vector2>(objectCount, Allocator.TempJob);

            // Populate position and mass arrays
            for (int i = 0; i < objectCount; i++)
            {
                positions[i] = Simulatables[i].position;
                masses[i] = Simulatables[i].mass;
            }

            // Create and schedule the job
            GravityJob gravityJob = new GravityJob
            {
                positions = positions,
                masses = masses,
                forces = forces,
                gravitationalConstant = gravitationalConstantt,
                minDistance = minDistance
            };

            JobHandle jobHandle = gravityJob.Schedule(objectCount, 64);
            jobHandle.Complete();

            // Apply the calculated forces
            for (int i = 0; i < objectCount; i++)
            {
                var v = Simulatables[i];
                
                v.AddForce(forces[i]);
                v.AddForce(v.transform.position.normalized * -FindObjectOfType<Manager>().centralize);
            }

            // Dispose of NativeArrays
            positions.Dispose();
            masses.Dispose();
            forces.Dispose();
        }
        
        [BurstCompile]
        public struct GravityJob : IJobParallelFor
        {
            [ReadOnly] public NativeArray<Vector2> positions;
            [ReadOnly] public NativeArray<float> masses;
            public NativeArray<Vector2> forces;

            public float gravitationalConstant;
            public float minDistance;

            public void Execute(int i)
            {
                Vector2 forceSum = Vector2.zero;
                Vector2 positionI = positions[i];
                float massI = masses[i];

                for (int j = 0; j < positions.Length; j++)
                {
                    if (i == j) continue; // Skip self

                    Vector2 positionJ = positions[j];
                    float massJ = masses[j];

                    Vector2 direction = positionJ - positionI;
                    float distance = Mathf.Max(direction.magnitude, minDistance);

                    // Inverse square law for gravity
                    float forceMagnitude = gravitationalConstant * (massI * massJ) / (distance * distance);
                    Vector2 force = direction.normalized * forceMagnitude;

                    forceSum += force;
                }

                forces[i] = forceSum;
            }
        }
    }
}