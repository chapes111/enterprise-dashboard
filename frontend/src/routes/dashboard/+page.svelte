<script lang="ts">
	import { superForm } from 'sveltekit-superforms';
	import type { PageData } from './$types';

	let { data }: { data: PageData } = $props();

	const {
		form: formData,
		errors,
		constraints,
		enhance,
		delayed,
		message
	} = superForm(data.form, {
		resetForm: true,
		onUpdated({ form }) {
			console.log('Client updated. Message:', form.message, 'Valid:', form.valid);
		},
		onError({ result }) {
			console.error('Client form error:', result);
		}
	});
</script>

<main class="mx-auto max-w-4xl space-y-8 p-6">
	<h1 class="text-2xl font-bold">Metrics Dashboard</h1>

	{#if $message}
		<div class="rounded border border-blue-300 bg-blue-100 p-3 text-blue-800">
			{$message}
		</div>
	{/if}

	<!-- Metric Submission Form -->
	<form method="POST" use:enhance class="space-y-4 rounded border bg-slate-50 p-4">
		<div>
			<label for="metricName" class="block font-medium">Metric Name</label>
			<input
				type="text"
				name="metricName"
				id="metricName"
				bind:value={$formData.metricName}
				{...$constraints.metricName}
				class="w-full rounded border p-2"
			/>
			{#if $errors.metricName}<span class="text-sm text-red-500">{$errors.metricName}</span>{/if}
		</div>

		<div>
			<label for="value" class="block font-medium">Value</label>
			<input
				type="number"
				step="any"
				name="value"
				id="value"
				bind:value={$formData.value}
				{...$constraints.value}
				class="w-full rounded border p-2"
			/>
			{#if $errors.value}<span class="text-sm text-red-500">{$errors.value}</span>{/if}
		</div>

		<div>
			<label for="tenantId" class="block font-medium">Tenant Id</label>
			<input
				type="text"
				name="tenantId"
				id="tenantId"
				bind:value={$formData.tenantId}
				{...$constraints.tenantId}
				class="w-full rounded border p-2"
			/>
			{#if $errors.tenantId}<span class="text-sm text-red-500">{$errors.tenantId}</span>{/if}
		</div>

		<button type="submit" class="rounded bg-blue-600 px-4 py-2 text-white">
			{$delayed ? 'Submitting...' : 'Add Metric'}
		</button>
	</form>

	<!-- Metrics Display Table -->
	<selection class="space-y-2">
		<h2 class="text-xl font-semibold">Live Metrics</h2>
		<table class="w-full border-collapse border text-left">
			<thead>
				<tr class="border-b bg-slate-100">
					<th class="p-2">Name</th>
					<th class="p-2">Value</th>
					<th class="p-2">Tenant</th>
				</tr>
			</thead>
			<tbody>
				{#each data.metrics as metric}
					<tr class="border-b">
						<td class="p-2">{metric.metricName}</td>
						<td class="p-2">{metric.value}</td>
						<td class="p-2">{metric.tenantId}</td>
					</tr>
				{:else}
					<tr><td colspan="3" class="p-2 text-gray-500">No metrics found.</td></tr>
				{/each}
			</tbody>
		</table>
	</selection>
</main>
